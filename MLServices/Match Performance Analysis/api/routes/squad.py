"""api/routes/squad.py — Squad Overview batch endpoint"""
from fastapi import APIRouter, HTTPException, Query
from typing import Optional
import pandas as pd
import os
from api.routes._shared import _load, _sf, _si, GRANULAR_LABELS, GRANULAR_POSITIONS, CLUSTER_SHORT
from config import TARGET_TEAM

router = APIRouter()

TEAM_NAME = os.environ.get("TARGET_TEAM", TARGET_TEAM)

def _p90(v, minutes=None):
    if minutes:
        return round(v / minutes * 90, 4) if minutes else None
    return round(float(v), 4) if v else None

POS_LABEL_MAP = {"GK": "GK", "Defender": "DF", "Midfielder": "MF", "Attacker": "FW"}

COARSE_TO_GRANULAR = {
    "GK": "Goalkeeper", "Defender": "Center Back",
    "Midfielder": "Central Midfielder", "Attacker": "Winger",
}


@router.get("/player/squad-scores")
def get_squad_scores(match_id: Optional[int] = Query(None), season: Optional[str] = Query(None)):
    d = _load(season=season)
    sc = d["scores"]
    cf = d["computed"]
    mt = d["matches"]
    pr = d.get("position_scores", None)

    # Determine match context
    team_col = "team_name" if "team_name" in sc.columns else "team"
    squad_ids = sc.loc[
        sc[team_col].astype(str).str.contains(TEAM_NAME, case=False, na=False),
        "player_id"
    ].unique()
    squad_match_ids = sc["match_id"][sc["player_id"].isin(squad_ids)].unique()

    if match_id is None:
        match_candidates = sorted(set(squad_match_ids) & set(mt["match_id"].unique()))
        if match_candidates:
            max_week = int(mt.loc[mt["match_id"].isin(match_candidates), "match_week"].max())
            best = mt[(mt["match_id"].isin(match_candidates)) & (mt["match_week"] == max_week)]
            match_id = int(best["match_id"].iloc[0]) if len(best) else int(match_candidates[-1])
        else:
            match_id = int(sc["match_id"].max())

    # Verify match exists in scores
    match_scores = sc[sc["match_id"] == match_id]
    if not len(match_scores):
        raise HTTPException(404, f"Match {match_id} not found in score data")

    # Match context from matches table
    match_row = mt[mt["match_id"] == match_id]
    match_context = {}
    if len(match_row):
        r = match_row.iloc[0]
        match_context = {
            "match_id": int(match_id),
            "match_date": str(r.get("match_date", "")),
            "home_team": str(r.get("home_team", "")),
            "away_team": str(r.get("away_team", "")),
            "home_score": _si(r.get("home_score")),
            "away_score": _si(r.get("away_score")),
            "match_week": _si(r.get("match_week")),
        }

    # Squad players for this match
    squad_ms = match_scores[match_scores["player_id"].isin(squad_ids)]
    squad_ids_this = squad_ms["player_id"].unique()

    # Position ratings for this match
    pr_match = pr[pr["match_id"] == match_id] if pr is not None and "match_id" in pr.columns else None

    # Build per-player rows
    players = []
    for pid in squad_ids_this:
        row = squad_ms[squad_ms["player_id"] == pid]
        if not len(row):
            continue
        r = row.iloc[0]
        pname = str(r["player_name"])
        pgroup = str(r.get("position_group", "Unknown"))
        pos_granular = str(r.get("position_granular", ""))
        if not pos_granular or pos_granular == "Unknown":
            pos_granular = COARSE_TO_GRANULAR.get(pgroup, "Central Midfielder")
        pos_short = GRANULAR_LABELS.get(pos_granular, pgroup[:2].upper())

        # Stats (computed features) for this match
        cf_row = cf[(cf["player_id"] == pid) & (cf["match_id"] == match_id)]
        cf_r = cf_row.iloc[0] if len(cf_row) else None

        # Position rating for this match
        pr_row = pr_match[pr_match["player_id"] == pid] if pr_match is not None else None
        pr_r = pr_row.iloc[0] if pr_row is not None and len(pr_row) else None

        # History for last-5 sparkline and trend value
        hist = sc[(sc["player_id"] == pid) & (sc[team_col].astype(str).str.contains(TEAM_NAME, case=False, na=False))]
        hist_sorted = hist.sort_values("match_id")
        last5 = [round(float(x), 2) for x in hist_sorted["overall_score"].tail(5).tolist()]

        # Trend value: compare last score to average of previous 3
        trend_scores = hist_sorted["overall_score"].tolist()
        trend_val = 0.0
        if len(trend_scores) >= 2:
            last_val = trend_scores[-1]
            prev_avg = sum(trend_scores[:-1]) / len(trend_scores[:-1])
            trend_val = round(last_val - prev_avg, 2)

        players.append({
            "player_id": _si(pid),
            "player_name": pname,
            "team_name": str(r.get("team_name", "")),
            "position_group": pgroup,
            "position_label": POS_LABEL_MAP.get(pgroup, pgroup[:2].upper()) if pgroup != "Unknown" else "—",
            "position_granular": pos_granular,
            "position_short": pos_short,
            "score": _sf(pr_r.get("score")) if pr_r is not None and pr_r.get("score") is not None else _sf(r.get("score")),
            "scores": {
                "passing": _sf(r.get("passing_score")),
                "shooting": _sf(r.get("shooting_score")),
                "positioning": _sf(r.get("positioning_score")),
                "pressing": _sf(r.get("pressing_score")),
                "movement": _sf(r.get("movement_score")),
            },
            "vaep_rating": _sf(r.get("vaep_rating")),
            "total_xg": _sf(cf_r.get("total_xg")) if cf_r is not None else None,
            "pass_accuracy": _sf(cf_r.get("pass_accuracy")) if cf_r is not None else None,
            "dribble_success_rate": _sf(cf_r.get("dribble_success_rate")) if cf_r is not None else None,
            "score_label": str(pr_r.get("score_label", "")) if pr_r is not None else "",
            "position_fit_score": _sf(r.get("position_fit_score")),
            "player_cluster": CLUSTER_SHORT.get(str(r.get("player_cluster", "")), "Unknown"),
            "performance_trend": str(r.get("performance_trend", "Stable")),
            "trend_value": trend_val,
            "last_5_scores": last5,
        })

    if not players:
        raise HTTPException(404, f"No {TEAM_NAME} players found for match {match_id}")

    players.sort(key=lambda p: p["score"] or 0, reverse=True)

    # Team stats
    squad_cf = cf[(cf["match_id"] == match_id) & (cf["player_id"].isin(squad_ids_this))]
    team_stats = {
        "total_passes": _si(squad_cf["total_passes"].sum()) if "total_passes" in squad_cf.columns else None,
        "total_shots": _si(squad_cf["total_shots"].sum()) if "total_shots" in squad_cf.columns else None,
        "shots_on_target": _si(squad_cf["shots_on_target"].sum()) if "shots_on_target" in squad_cf.columns else None,
        "total_xg": _sf(squad_cf["total_xg"].sum()) if "total_xg" in squad_cf.columns else None,
        "pass_accuracy": _sf(squad_cf["pass_accuracy"].mean()) if "pass_accuracy" in squad_cf.columns else None,
        "total_pressures": _si(squad_cf["total_pressures"].sum()) if "total_pressures" in squad_cf.columns else None,
        "pressure_regains": _si(squad_cf["pressure_regains"].sum()) if "pressure_regains" in squad_cf.columns else None,
        "total_dribbles": _si(squad_cf["total_dribbles"].sum()) if "total_dribbles" in squad_cf.columns else None,
        "dribble_success_pct": _sf(
            (squad_cf["successful_dribbles"].sum() / squad_cf["total_dribbles"].sum() * 100)
            if "successful_dribbles" in squad_cf.columns and squad_cf["total_dribbles"].sum() > 0
            else None
        ),
        "team_vaep": _sf(squad_ms["vaep_rating"].sum()) if "vaep_rating" in squad_ms.columns else None,
        "possession_pct": None,
    }

    # Possession estimate: total-action ratio (passes + carries + dribbles + shots)
    opp_ms = match_scores[~match_scores["player_id"].isin(squad_ids)]
    if len(opp_ms):
        opp_cf = cf[(cf["match_id"] == match_id) & (cf["player_id"].isin(opp_ms["player_id"].unique()))]
        def _total_actions(df):
            return float(df["total_passes"].sum() + df["total_carries"].sum() + df["total_dribbles"].sum() + df["total_shots"].sum()) if all(c in df.columns for c in ["total_passes","total_carries","total_dribbles","total_shots"]) else None
        squad_acts = _total_actions(squad_cf)
        opp_acts = _total_actions(opp_cf)
        total_a = (squad_acts or 0) + (opp_acts or 0)
        if squad_acts is not None and opp_acts is not None and total_a > 0:
            team_stats["possession_pct"] = round((squad_acts / total_a) * 100, 1)

    # Insights
    top = max(players, key=lambda p: p["score"] or 0) if players else None
    insights = {"top_performer": None, "most_improved": None, "declining": None, "below_baseline_count": 0}

    if top:
        insights["top_performer"] = {
            "player_name": top["player_name"],
            "score": top["score"],
        }

    # Most improved: highest trend_value
    improved = max(players, key=lambda p: p["trend_value"]) if players else None
    if improved and improved["trend_value"] > 0:
        insights["most_improved"] = {
            "player_name": improved["player_name"],
            "delta": improved["trend_value"],
        }

    # Declining: lowest trend_value
    declining = min(players, key=lambda p: p["trend_value"]) if players else None
    if declining and declining["trend_value"] < 0:
        insights["declining"] = {
            "player_name": declining["player_name"],
            "delta": declining["trend_value"],
        }

    # Below baseline: recent trend is significantly negative
    insights["below_baseline_count"] = sum(
        1 for p in players if p.get("trend_value", 0) < -0.1
    )

    # Available matches for selector (most recent first by season week)
    avail = mt[mt["match_id"].isin(squad_match_ids)].sort_values("match_week", ascending=False)
    available_matches = [
        {
            "match_id": _si(r["match_id"]),
            "match_date": str(r.get("match_date", "")),
            "home_team": str(r.get("home_team", "")),
            "away_team": str(r.get("away_team", "")),
            "home_score": _si(r.get("home_score")),
            "away_score": _si(r.get("away_score")),
            "match_week": _si(r.get("match_week")),
        }
        for _, r in avail.iterrows()
    ]

    return {
        "match_context": match_context,
        "team_stats": team_stats,
        "players": players,
        "insights": insights,
        "available_matches": available_matches,
    }


@router.get("/player/season-players")
def get_season_players(
    season: str = Query(..., description="Season label e.g. 2015/2016"),
    position: Optional[str] = Query(None, description="Filter by position group: GK, DF, MF, FW"),
):
    d = _load(season=season)
    sc = d["scores"]
    cf = d["computed"]
    kpi = d.get("position_scores", None)

    # Filter Barcelona players for this season
    squad_ms = sc[sc["team_name"].astype(str).str.contains(TEAM_NAME, case=False, na=False)]
    squad_ms = squad_ms[squad_ms["season_label"] == season]

    if not len(squad_ms):
        raise HTTPException(404, f"No Barcelona players found for season {season}")

    # Build per-player season aggregates
    players = []
    for pid in squad_ms["player_id"].unique():
        p_rows = squad_ms[squad_ms["player_id"] == pid]
        r = p_rows.iloc[0]
        pname = str(r["player_name"])
        pgroup = str(r.get("position_group", "Unknown"))
        if position:
            POS_CODE_MAP = {"FW": "Attacker", "MF": "Midfielder", "DF": "Defender", "GK": "GK"}
            mapped = POS_CODE_MAP.get(position.upper(), position.upper())
            if pgroup.upper() != mapped.upper():
                continue
        pos_granular = str(r.get("position_granular", ""))
        if not pos_granular or pos_granular == "Unknown":
            pos_granular = COARSE_TO_GRANULAR.get(pgroup, "Central Midfielder")
        match_ids = p_rows["match_id"].unique()

        # Season computed features
        cf_season = cf[(cf["player_id"] == pid) & (cf["match_id"].isin(match_ids))]
        total_minutes = float(cf_season["minutes_played"].sum()) if len(cf_season) and "minutes_played" in cf_season.columns else 0

        # Season position scores
        kpi_season = kpi[(kpi["player_id"] == pid) & (kpi["match_id"].isin(match_ids))] if kpi is not None and "player_id" in kpi.columns else pd.DataFrame()
        dim_cols = [c for c in kpi_season.columns if c.startswith("score_") and c != "score_label"]
        kpi_dims_avg = {c: _sf(kpi_season[c].mean()) for c in dim_cols if len(kpi_season[c].dropna())} if len(kpi_season) else {}
        avg_score = _sf(kpi_season["score"].mean()) if len(kpi_season) and "score" in kpi_season.columns else None

        def _cf(col):
            return col in cf_season.columns

        sort_avg = _sf(p_rows["overall_score"].mean())
        players.append({
            "player_id": _si(pid),
            "player_name": pname,
            "position_group": pgroup,
            "position_granular": pos_granular,
            "position_short": GRANULAR_LABELS.get(pos_granular, pgroup[:2].upper()),
            "matches_played": _si(len(p_rows)),
            "avg_score": avg_score,
            "avg_minutes": _sf(cf_season["minutes_played"].mean()) if len(cf_season) and "minutes_played" in cf_season.columns else None,
            "avg_vaep_rating": _sf(p_rows["vaep_rating"].mean()),
            "score_dimensions": kpi_dims_avg,
            "_sort_avg": sort_avg,
            "goals_per90": _p90(float(cf_season["goals"].sum()), total_minutes) if len(cf_season) and _cf("goals") else None,
            "assists_per90": _p90(float(cf_season["assists"].sum()), total_minutes) if len(cf_season) and _cf("assists") else None,
            "shots_per90": _p90(float(cf_season["total_shots"].sum()), total_minutes) if len(cf_season) and _cf("total_shots") else None,
            "passes_per90": _p90(float(cf_season["total_passes"].sum()), total_minutes) if len(cf_season) and _cf("total_passes") else None,
            "pass_accuracy": _sf(cf_season["pass_accuracy"].mean()) if len(cf_season) and _cf("pass_accuracy") else None,
            "progressive_passes_per90": _p90(float(cf_season["progressive_passes"].sum()), total_minutes) if len(cf_season) and _cf("progressive_passes") else None,
            "progressive_carries_per90": _p90(float(cf_season["progressive_carries"].sum()), total_minutes) if len(cf_season) and _cf("progressive_carries") else None,
            "dribbles_per90": _p90(float(cf_season["successful_dribbles"].sum()), total_minutes) if len(cf_season) and _cf("successful_dribbles") else None,
            "pressure_regains_per90": _p90(float(cf_season["pressure_regains"].sum()), total_minutes) if len(cf_season) and _cf("pressure_regains") else None,
            "chances_created_per90": _p90(float(cf_season["chances_created"].sum()), total_minutes) if len(cf_season) and _cf("chances_created") else None,
            "ball_receipts_per90": _p90(float(cf_season["ball_receipts"].sum()), total_minutes) if len(cf_season) and _cf("ball_receipts") else None,
            "defensive_actions_per90": _p90(
                float(cf_season["interceptions"].sum() + cf_season["clearances"].sum() + cf_season["blocks"].sum()), total_minutes
            ) if len(cf_season) and _cf("interceptions") else None,
            "duels_per90": _p90(float(cf_season["duels_total"].sum()), total_minutes) if len(cf_season) and _cf("duels_total") else None,
            "saves_per90": _p90(float(cf_season["saves"].sum()), total_minutes) if len(cf_season) and _cf("saves") else None,
            "save_pct": _sf(cf_season["save_pct"].mean()) if len(cf_season) and _cf("save_pct") else None,
            "goals_conceded_per90": _p90(float(cf_season["goals_conceded"].sum()), total_minutes) if len(cf_season) and _cf("goals_conceded") else None,
            "shot_accuracy": _sf(cf_season["shot_accuracy"].mean()) if len(cf_season) and _cf("shot_accuracy") else None,
            "xg_overperformance": _sf(cf_season["xg_overperformance"].mean()) if len(cf_season) and _cf("xg_overperformance") else None,
        })

    players.sort(key=lambda p: p["_sort_avg"] or 0, reverse=True)
    for p in players:
        del p["_sort_avg"]

    return {
        "season": season,
        "team": TEAM_NAME,
        "player_count": len(players),
        "players": players,
    }
