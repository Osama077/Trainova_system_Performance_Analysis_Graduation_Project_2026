# Match Performance Analysis — MLServices

An end-to-end **Football (Soccer) Match Performance Analysis System** that processes raw StatsBomb event data through a 7-stage ML pipeline, produces professional player ratings (0–10) via multiple scoring models, and exposes 51 REST API endpoints consumed by a 27-component React frontend.

Built for **Barcelona** across 11 La Liga seasons (2010/11 – 2020/21).

---

## Architecture

```
StatsBomb Events → Data Loader → Feature Engineering → xG Model → VAEP Model → Scoring Model → Metadata → Position KPI
                                                                                                                       ↘
FastAPI (51 endpoints) ← model_scores.parquet ← data/model_scores.parquet ← position_scores.parquet ← position_kpi.parquet
       ↓
React Frontend (27 components)
       ↓
Coach / Analyst
```

| Layer | Technology | Location |
|-------|-----------|----------|
| **Pipeline** | Python 3.14, pandas, numpy, LightGBM, XGBoost | `pipeline/` |
| **API** | FastAPI, uvicorn | `api/` |
| **Frontend** | React 18, Tailwind CSS, Recharts, Axios | `front-end/` |
| **Data** | Parquet + CSV | `data/` |
| **Models** | Pickle + JSON | `models/` |

---

## ML Pipeline (7 Steps)

All steps are orchestrated by `run_pipeline.py`. Run via `python run_pipeline.py --mode pipeline` or step-by-step with `--step N`.

| Step | File | Purpose | Produces |
|------|------|---------|----------|
| **1. Data Loading** | `pipeline/data_loader.py` | Download StatsBomb events, lineups, matches; convert to SPADL format | `matches.parquet`, `events_clean.parquet`, `lineups.parquet`, `spadl_actions.parquet`, `shots_for_xg.parquet`, per-season files |
| **2. Feature Engineering** | `pipeline/feature_engineering.py` | Compute 50+ per-player per-match features across 8 dimensions | `computed_features.parquet` |
| **3. xG Model** | `pipeline/xg_model.py` | Train LightGBM expected-goals model on all StatsBomb shots | `xg_model.txt`, `barca_shots_with_xg.parquet` |
| **4. VAEP Model** | `pipeline/vaep_model.py` | Train XGBoost Valuing Actions by Estimating Probabilities (offensive + defensive) | `vaep_offensive_model.json`, `vaep_defensive_model.json`, `player_vaep_ratings.parquet` |
| **5. Scoring Model (V2)** | `pipeline/scoring_model.py` | 4-pillar contribution scoring → 0–10 rating with percentiles, trends, clusters | `model_scores.parquet`, `position_benchmarks.parquet`, `gmm_model.pkl` |
| **6. Metadata Loader** | `pipeline/metadata_loader.py` | Build player identity catalog (positions, foot, career stats) | `player_info.parquet` |
| **7. Position KPI** | `pipeline/position_kpi.py` | Granular (8-position) percentile-based KPI ratings with excess-only formula | `position_scores.parquet`, `position_kpi.parquet` |

### Step 2 — Feature Engineering Dimensions

| Dimension | Key Features | Source Events |
|-----------|-------------|---------------|
| **Passing** | total_passes, pass_accuracy, progressive_passes, avg_pass_length | Pass events |
| **Shooting** | total_shots, goals, total_xg, xg_per_shot, xg_overperformance, shot_accuracy | Shot events + xG predictions |
| **Positioning** | avg_position_x/y, position_deviation, attacking_tendency, std_position | All event coordinates |
| **Pressing** | total_pressures, pressure_regains, pressing_efficiency | Pressure events |
| **Movement** | total_carries, carry_distance, progressive_carries, total_dribbles, successful_dribbles, dribble_success_rate | Carry + Dribble events |
| **Physical** | distance_covered, total_actions, activity_drop_2nd_half, intensity metrics | Segment-interpolated distances |
| **Behavioral** | fouls_committed, fouls_won, yellow_cards, red_cards, ball_receipts, ball_retention_rate | Foul + Card + Receipt events |
| **Goalkeeper** | saves, shots_faced, goals_conceded, save_pct, goals_prevented | GK-specific events |

\* `distance_covered` estimated as `min(15000, minutes_played × 80 + segment_distance × 3)` — accounts for off-ball running absent from event data.

---

## Scoring Models

### V2 Contribution Model (`scoring_model.py`)

4-pillar scoring with position-specific weights:

| Position | Offensive | Defensive | Possession | Event Value |
|----------|-----------|-----------|------------|-------------|
| **Attacker** | 0.50 | 0.05 | 0.20 | 0.25 |
| **Midfielder** | 0.25 | 0.15 | 0.35 | 0.25 |
| **Defender** | 0.10 | 0.40 | 0.25 | 0.25 |
| **Goalkeeper** | 0.02 | 0.50 | 0.23 | 0.25 |

Rating formula: **6.0 + Σ max(0, sub_score − 5.0) × weight** — players start at 6.0 (baseline for playing) and only excess above neutral (5.0) contributes upward. All 12 score columns clipped at ≥ 6.0.

### KPI Model (`position_kpi.py`)

8 granular positions with percentile-based scoring:

| Granular Position | KPI Short | Key Features & Weights |
|-------------------|-----------|----------------------|
| **Goalkeeper** | GK | save_pct (0.35), goals_prevented (0.30), goals_conceded_per90 (0.20), pass_accuracy (0.15) |
| **Center Back** | CB | defensive_actions_per90 (0.25), duels_total_per90 (0.15), pass_accuracy (0.20), progressive_passes_per90 (0.15) |
| **Full Back** | FB | progressive_carries_per90 (0.20), successful_dribbles_per90 (0.15), defensive_actions_per90 (0.20), chances_created_per90 (0.15) |
| **Defensive Midfielder** | DMF | defensive_actions_per90 (0.25), pass_accuracy (0.20), pressure_regains_per90 (0.20), progressive_passes_per90 (0.15) |
| **Central Midfielder** | CMF | pass_accuracy (0.20), total_passes_per90 (0.20), chances_created_per90 (0.15), progressive_passes_per90 (0.15) |
| **Attacking Midfielder** | AMF | chances_created_per90 (0.25), goals_per90 (0.20), successful_dribbles_per90 (0.15), shot_accuracy (0.15) |
| **Winger** | WG | goals_per90 (0.30), chances_created_per90 (0.20), successful_dribbles_per90 (0.15), shot_accuracy (0.15) |
| **Striker** | ST | goals_per90 (0.35), shot_accuracy (0.20), xg_overperformance (0.15), chances_created_per90 (0.10) |

KPI formula: **6.0 + Σ max(0, dim_score − 5.0) × weight** — same excess-only logic as V2.

### Legacy Dimension Scoring

7 dimensions with position-specific weights (backward compatible):

- **Passing Score**, **Shooting Score**, **Positioning Score**, **Pressing Score**, **Movement Score**, **Physical Score**, **Behavioral Score**
- Plus **Position Fit Score** (percentile match to position benchmark)

---

## API Endpoints (51 total)

All under `/api/v1`. Docs at `/docs` (Swagger) and `/redoc`.

### Player Endpoints

| Method | Path | Description |
|--------|------|-------------|
| GET | `/player/list` | List all players with team |
| GET | `/player/{player_id}/score` | Player match score breakdown |
| GET | `/player/{player_id}/stats` | Player raw match stats |
| GET | `/player/{player_id}/history` | Player match history |
| GET | `/player/compare` | Compare N players side-by-side |
| GET | `/player/head-to-head` | Head-to-head: 2 players across season/match/last5 |
| GET | `/player/dashboard/{name}` | 9 base64 chart images |
| GET | `/player/dashboard-data/{name}` | Raw chart JSON data |
| GET | `/player/season/list` | Available seasons |
| GET | `/player/{player_id}/evolution` | Year-over-year career evolution |
| GET | `/player/season-trends` | Season dashboard trends |
| GET | `/player/match-log` | Match log with detail |
| GET | `/player/tactical-board` | Tactical formation + pass network |
| GET | `/player/squad-scores` | Squad per-player scores for a match |
| GET | `/player/season-players` | Season aggregate per-player stats |
| **GET** | **`/player/profile/{player_name}`** | **Comprehensive player profile** (all fields below) |

### Comprehensive Profile Response Fields

| Field | Contents |
|-------|----------|
| `player_info` | ID, name, initials, positions, cluster, trend, match_score |
| `match_context` | Match info, opponent, result, date |
| `match_scores` | 7 dimension scores for current match |
| `match_stats` | Passing, shooting, pressing stats + GK-specific |
| `percentiles` | Team/league/position percentile ranks |
| `season_stats` | Total_matches, matches_above_7, delta_vs_avg, best/worst match |
| `squad_mates` | Fellow Barcelona players in this match |
| `radar_data` | Labels + match_values + season_values (7-dim radar) |
| `trend_data` | Per-match scores + rolling_avg + 7 dimension scores |
| `charts` | Heatmap, pass map, shot/save map as base64 |
| `match_log` | All matches with ml_score, delta_vs_avg, stats |
| `timeline_events` | Key match events |
| `available_matches` | Match selector options |

### Squad & Team Endpoints

| Method | Path | Description |
|--------|------|-------------|
| GET | `/team/{team_id}/summary` | Team summary with top performer |
| GET | `/team/{team_id}/heatmap` | Team event spatial heatmap |

### Match Endpoints

| Method | Path | Description |
|--------|------|-------------|
| GET | `/match/list` | All matches sorted by date |
| GET | `/match/{match_id}/analysis-complete` | Full match: tactics, stats, events, ratings, pass network |
| GET | `/match/{match_id}/report` | Match summary report |
| GET | `/match/{match_id}/events` | Paginated match events |

### Advanced Analysis Endpoints

| Method | Path | Description |
|--------|------|-------------|
| GET | `/player/{player_id}/advanced` | All 6 ML engines combined |
| GET | `/player/{player_id}/forecast` | Ridge + GBR next-match prediction |
| GET | `/player/{player_id}/anomalies` | IsolationForest + z-score anomaly detection |
| GET | `/player/{player_id}/similar` | PCA + cosine-similarity player matching |
| GET | `/player/{player_id}/consistency` | MAD + CV + autocorrelation consistency |
| GET | `/player/{player_id}/momentum` | EWMA + change-point detection momentum |
| GET | `/player/{player_id}/injury-risk` | ACWR + fatigue + workload injury risk |
| GET | `/analysis/top-performers` | Leaderboard by score/momentum/consistency |

### Coaching & Prediction Endpoints

| Method | Path | Description |
|--------|------|-------------|
| GET | `/coaching/squad` | Squad tactical advice sans match |
| GET | `/coaching/squad/{match_id}` | Match-specific squad coaching |
| GET | `/coaching/player/{player_id}` | Player coaching guidance |
| GET | `/coaching/player/{player_id}/comprehensive` | All-in-one player coaching |
| GET | `/predict/player/{player_id}` | Next-match technical + physical prediction |
| GET | `/predict/squad` | Full squad next-match prediction |
| GET | `/validate/metrics` | Data validation audit report |
| GET | `/validate/formulas` | Formula documentation |

### KPI Scoring Endpoints

| Method | Path | Description |
|--------|------|-------------|
| GET | `/scores/positions` | List 8 granular positions |
| GET | `/scores/player/{player_id}` | Player granular KPI scores |
| GET | `/scores/compare` | Compare granular KPI scores |
| GET | `/scores/distribution/{position}` | Position score distribution |
| GET | `/scores/rankings` | Position-relative score rankings |

### Metadata Endpoints

| Method | Path | Description |
|--------|------|-------------|
| GET | `/metadata/players` | All players with metadata filters |
| GET | `/metadata/players/{player_id}` | Single player full metadata |
| GET | `/metadata/player/search` | Fuzzy name search |

### Other Endpoints

| Method | Path | Description |
|--------|------|-------------|
| GET | `/` | Root health check |
| POST | `/analyze/match/{match_id}` | Match analysis status |
| POST | `/analyze/season` | Season analysis status |
| GET | `/benchmark/{position_group}` | Position benchmarks |

---

## Frontend Components (27)

All under `front-end/src/components/`.

| Component | File | API Dependency |
|-----------|------|----------------|
| **HomePage** | `HomePage.js` | None (static) |
| **Navigation** | `Navigation.js` | Health + Seasons |
| **PlayerList** | `PlayerList.js` | `PlayerAPI.getPlayerList` |
| **PlayerProfile** | `PlayerProfile.js` | `PlayerProfileAPI.getProfile` |
| **PlayerDashboard** | `PlayerDashboard.js` | `PlayerAPI.getPlayerDashboard` |
| **PlayerComparison** | `PlayerComparison.js` | `PlayerAPI.comparePlayer` |
| **PlayerCompare** | `PlayerCompare.js` | `PlayerAPI.comparePlayer` |
| **PlayerAnimatedAnalysis** | `PlayerAnimatedAnalysis.js` | `PlayerAPI.getPlayerDashboard` |
| **PlayerAnomalies** | `PlayerAnomalies.js` | `AdvancedAnalysisAPI.getAnomalies` |
| **PlayerConsistency** | `PlayerConsistency.js` | `AdvancedAnalysisAPI.getConsistency` |
| **PlayerForecast** | `PlayerForecast.js` | `AdvancedAnalysisAPI.getForecast` |
| **PlayerInjuryRisk** | `PlayerInjuryRisk.js` | `AdvancedAnalysisAPI.getInjuryRisk` |
| **PlayerMomentum** | `PlayerMomentum.js` | `AdvancedAnalysisAPI.getMomentum` |
| **PlayerSimilarity** | `PlayerSimilarity.js` | `AdvancedAnalysisAPI.getSimilarPlayers` |
| **PlayerProfiles** | `PlayerProfiles.js` | `SquadAPI.getSquadOverview` |
| **SquadOverview** | `SquadOverview.js` | `SquadAPI.getSquadOverview` |
| **PositionDashboard** | `PositionDashboard.js` | `ScoreStatsAPI.getScoreStats` |
| **CoachingInsights** | `CoachingInsights.js` | `CoachingAPI.getCoachingInsights` |
| **MatchLog** | `MatchLog.js` | `MatchLogAPI.getMatchLog` + `MatchAnalysisAPI` |
| **MatchPrediction** | `MatchPrediction.js` | `PredictionAPI` |
| **SeasonTrends** | `SeasonTrends.js` | `SeasonTrendsAPI.getSeasonTrends` |
| **TacticalBoard** | `TacticalBoard.js` | `TacticalBoardAPI.getTacticalBoard` |
| **TopPerformers** | `TopPerformers.js` | `AdvancedAnalysisAPI.getTopPerformers` |
| **APITester** | `APITester.js` | 6 API objects (debugging tool) |
| **WhatsNewPage** | `WhatsNewPage.js` | Metadata + Seasons + Evolution |
| **LoadingSpinner** | `LoadingSpinner.js` | None (UI) |
| **ErrorAlert** | `ErrorAlert.js` | None (UI) |

---

## Data Files

| File | Produced By | Contents |
|------|-------------|----------|
| `data/events_clean.parquet` | data_loader | All cleaned events |
| `data/matches.parquet` | data_loader | All match fixtures |
| `data/lineups.parquet` | data_loader | All lineups |
| `data/spadl_actions.parquet` | data_loader | SPADL-converted actions |
| `data/computed_features.parquet` | feature_engineering | 50+ features per player-match |
| `data/barca_shots_with_xg.parquet` | xg_model | Shots with predicted_xg |
| `data/player_vaep_ratings.parquet` | vaep_model | Player-match VAEP ratings |
| `data/model_scores.parquet` | scoring_model | Final V2 scores (60+ columns) |
| `data/position_scores.parquet` | position_kpi | Granular KPI scores |
| `data/position_kpi.parquet` | position_kpi | (Duplicate of above) |
| `data/position_benchmarks.parquet` | scoring_model | Position-group averages |
| `data/metadata/player_info.parquet` | metadata_loader | Player catalog |
| `data/seasons/{label}/*.parquet` | data_loader | Per-season data (11 seasons) |

---

## Key Improvements & Fixes Applied

| Issue | File(s) | Fix |
|-------|---------|-----|
| KPI scoring pulled scores down for weak performances | `pipeline/position_kpi.py:225-252` | Changed from averaging to **excess-only formula**: 6.0 + Σ max(0, dim − 5.0) × weight. Only above-neutral contributions raise the score. |
| Feature weights unrealistic | `pipeline/position_kpi.py:62-105` | Rebalanced all 8 positions: goals weight increased (Winger 0.20→0.30, Striker 0.30→0.35), dribbles reduced (Winger 0.25→0.15) |
| distance_covered showed 1.3km for 90min matches | `pipeline/feature_engineering.py:409-416` | New formula: min(15000, minutes × 80 + segment_distance × 3). Now 11.15km median for 90min |
| Scores below 6.0 for regular performers | `pipeline/scoring_model.py` | All `.clip(0, 10)` → `.clip(6.0, 10.0)`; coaching threshold < 5.0 → < 6.5 |
| Frontend dashes for match_score, season radar, trend rolling avg | `api/routes/player_profile.py` | Added 5 missing response fields (match_score, season_values, rolling_avg, season_stats, ml_score+delta_vs_avg) |
| PlayerList.js filter crash | `front-end/src/components/PlayerList.js:47` | Fixed typo: `debounce` → `debouncedSearch` |
| Port mismatch | `front-end/src/api.js:8` | 8001 → 8000 |
| Hardcoded "Barcelona" | 3 route files | Replaced with `config.TARGET_TEAM` |
| Missing player_profile.py and squad.py routes | `api/routes/` | Created new route files with comprehensive endpoints |
| CORS locked to localhost | `api/main.py` | Made configurable via `CORS_ORIGINS` env var |

---

## Value Proposition

| Capability | Value |
|------------|-------|
| **Player Ratings** | 0–10 scale with 6.0 floor, position-specific weights, 7 dimension breakdown, percentiles, trend classification |
| **Granular KPI** | 8-position-specific scoring with curated feature weights — a Winger is not graded like a Center Back |
| **xG Model** | LightGBM expected-goals trained on 100K+ shots across all StatsBomb competitions |
| **VAEP Model** | XGBoost-based action valuation — offensive and defensive contribution per event |
| **Player Clustering** | GMM identifies 5 archetypes (Creative Playmaker, Box-to-Box, Target Forward, Ball-Playing Defender, Pressing Machine) |
| **Advanced Analytics** | 6 ML engines: forecast, anomaly, similarity, consistency, momentum, injury risk |
| **Coaching Guidance** | Natural-language tactical advice translated from analytics |
| **Formation Reconstruction** | KMeans-based formation detection + pass network + SVG coordinates |
| **Match Prediction** | Ridge + EWMA blend for next-match technical/physical output |
| **Comprehensive API** | 51 endpoints — player, squad, team, match, coaching, prediction, validation |
| **Interactive Frontend** | 27 React components — profiles, comparison, dashboards, tactical boards, season trends |
| **11 Season History** | Barcelona La Liga 2010/11 – 2020/21: 10,691 player-match records across 400+ matches |

---

## Quick Start

```bash
# 1. Install Python dependencies
pip install -r requirements.txt

# 2. Install frontend dependencies
cd front-end && npm install && cd ..

# 3. Run full pipeline (downloads StatsBomb data)
python run_pipeline.py --mode pipeline

# 4. Start API server
python _run_api.py
# → API at http://localhost:8000
# → Docs at http://localhost:8000/docs

# 5. Start frontend
cd front-end && npm start
# → http://localhost:3000
```

### Docker

```bash
docker build -t match-performance-api .
docker run -p 8000:8000 match-performance-api
```

---

## Project Structure

```
MLServices/Match Performance Analysis/
├── api/
│   ├── main.py                        # FastAPI app + router registration
│   └── routes/
│       ├── _shared.py                 # Data loading, helpers, constants
│       ├── player.py                  # Player endpoints (12 routes)
│       ├── player_profile.py          # Comprehensive profile endpoint
│       ├── squad.py                   # Squad + season-players endpoints
│       ├── match.py                   # Match endpoints (4 routes)
│       ├── team.py                    # Team endpoints (2 routes)
│       ├── analysis.py                # Analysis status endpoints
│       ├── advanced_analysis.py       # 6 ML engine endpoints
│       ├── coaching.py                # Coaching guidance endpoints
│       ├── benchmark.py               # Position benchmark endpoint
│       ├── position_kpi_routes.py     # Granular KPI endpoints
│       └── metadata.py                # Player metadata endpoints
├── pipeline/
│   ├── data_loader.py                 # Step 1: StatsBomb data loading
│   ├── feature_engineering.py         # Step 2: 50+ feature computation
│   ├── xg_model.py                    # Step 3: xG model training
│   ├── vaep_model.py                  # Step 4: VAEP model training
│   ├── scoring_model.py               # Step 5: V2 scoring model
│   ├── metadata_loader.py             # Step 6: player metadata
│   ├── position_kpi.py                # Step 7: granular KPI scoring
│   ├── position_rating.py             # Alternative position rating engine
│   ├── formation_reconstruction.py    # Tactical formation detection
│   ├── advanced_analysis.py           # Forecast, anomaly, similarity, etc.
│   ├── coaching_guidance.py           # Decision support engine
│   ├── match_prediction.py            # Next-match predictor
│   └── data_validation.py             # Formula audit tool
├── front-end/
│   ├── src/
│   │   ├── api.js                     # API client (20 API objects)
│   │   ├── App.js                     # Main app router
│   │   └── components/               # 27 React components
│   └── package.json
├── data/                              # All parquet + CSV data files
├── models/                            # Trained ML models
├── config.py                          # Central configuration
├── run_pipeline.py                    # Pipeline orchestrator
├── _run_api.py                        # API server launcher
├── requirements.txt
└── Dockerfile
```

---

## Configuration

Key settings in `config.py`:

| Setting | Description | Default |
|---------|-------------|---------|
| `TARGET_TEAM` | Team to analyze | `"Barcelona"` |
| `SEASONS_LIST` | 11 La Liga seasons | 2010/11 – 2020/21 |
| `VAEP_WINDOW` | Actions for prediction window | 10 |
| `API_HOST` | Server bind address | `0.0.0.0` |
| `API_PORT` | Server port | 8000 |
| `CORS_ORIGINS` | Frontend origins (env var) | `http://localhost:3000,http://localhost:8000` |

---

## Technologies

- **Python 3.14** — pandas, numpy, scikit-learn, LightGBM, XGBoost, FastAPI, uvicorn
- **React 18** — Tailwind CSS, Recharts, Axios, Lucide icons
- **ML Models** — LightGBM (xG), XGBoost (VAEP), GMM (clustering), Ridge/GBR (forecast), IsolationForest (anomalies)
- **Data** — StatsBomb open-data, Parquet, SPADL format
- **Infrastructure** — Docker, uvicorn, npm
