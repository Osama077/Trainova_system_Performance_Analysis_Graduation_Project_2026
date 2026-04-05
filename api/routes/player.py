"""api/routes/player.py"""
from fastapi import APIRouter, HTTPException, Query
from typing import Optional
from api.routes._shared import _load_data, _sf, _si, _to_records
from visualizations.player_dashboard import generate_all_charts, get_player_list, get_player_chart_data

router = APIRouter()


@router.get("/player/list")
def list_players():
    """قائمة بأسماء كل اللاعبين المتاحين"""
    d = _load_data()
    scores_df = d["scores"]
    team_col = "team_name" if "team_name" in scores_df.columns else ("team" if "team" in scores_df.columns else None)
    base_cols = ["player_id", "player_name"] + ([team_col] if team_col else [])
    players_df = scores_df[base_cols].dropna(subset=["player_id", "player_name"]).drop_duplicates()
    if team_col:
        players_df = (
            players_df.sort_values("player_id")
            .groupby(["player_id", "player_name"], as_index=False)[team_col]
            .agg(lambda s: s.dropna().astype(str).mode().iat[0] if not s.dropna().empty else "Unknown")
        )
    players_df = players_df.sort_values("player_name")
    player_items = [
        {
            "player_id": _si(r["player_id"]),
            "player_name": str(r["player_name"]),
            "team_name": str(r.get(team_col, "Unknown")) if team_col else "Unknown",
        }
        for _, r in players_df.iterrows()
    ]
    teams = sorted({item["team_name"] for item in player_items if item.get("team_name")})
    return {
        "players": get_player_list(),
        "player_items": player_items,
        "teams": teams,
    }


@router.get("/player/{player_id}/score")
def get_score(player_id: int, match_id: Optional[int] = Query(None)):
    d  = _load_data()
    ps = d["scores"][d["scores"]["player_id"] == player_id]
    if not len(ps): raise HTTPException(404, f"Player {player_id} not found")

    if match_id:
        row = ps[ps["match_id"] == match_id]
        if not len(row): raise HTTPException(404, f"No data for match {match_id}")
        row = row.iloc[0]
    else:
        row = ps.merge(d["matches"][["match_id","match_date"]], on="match_id", how="left")\
                .sort_values("match_date").iloc[-1]

    return {
        "uuid":        str(row.get("uuid","")),
        "player_id":   _si(row["player_id"]),
        "player_name": str(row["player_name"]),
        "match_id":    _si(row["match_id"]),
        "position":    str(row.get("position_group","Unknown")),
        "scores": {
            "overall_score":      _sf(row["overall_score"]),
            "passing_score":      _sf(row["passing_score"]),
            "shooting_score":     _sf(row["shooting_score"]),
            "positioning_score":  _sf(row["positioning_score"]),
            "pressing_score":     _sf(row["pressing_score"]),
            "movement_score":     _sf(row["movement_score"]),
            "physical_score":     _sf(row["physical_score"]),
            "behavioral_score":   _sf(row["behavioral_score"]),
            "position_fit_score": _sf(row.get("position_fit_score")),
        },
        "percentiles": {
            "in_team":     _sf(row.get("percentile_in_team")),
            "in_league":   _sf(row.get("percentile_in_league")),
            "in_position": _sf(row.get("percentile_in_position")),
        },
        "vaep": {
            "vaep_rating":     _sf(row.get("vaep_rating")),
            "offensive_value": _sf(row.get("offensive_value")),
            "defensive_value": _sf(row.get("defensive_value")),
        },
        "performance_trend": str(row.get("performance_trend","Stable")),
        "player_cluster":    str(row.get("player_cluster","Unknown")),
    }


@router.get("/player/{player_id}/stats")
def get_stats(player_id: int, match_id: Optional[int] = Query(None)):
    d  = _load_data()
    pf = d["computed"][d["computed"]["player_id"] == player_id]
    if not len(pf): raise HTTPException(404, f"Player {player_id} not found")
    row = pf[pf["match_id"] == match_id].iloc[0] if match_id else pf.iloc[-1]

    return {
        "uuid":        str(row.get("uuid","")),
        "player_id":   _si(row["player_id"]),
        "player_name": str(row["player_name"]),
        "match_id":    _si(row["match_id"]),
        "passing":    {"total_passes":_si(row.get("total_passes")),
                       "pass_accuracy":_sf(row.get("pass_accuracy")),
                       "progressive_passes":_si(row.get("progressive_passes")),
                       "passes_under_pressure":_si(row.get("passes_under_pressure"))},
        "shooting":   {"total_shots":_si(row.get("total_shots")),
                       "shots_on_target":_si(row.get("shots_on_target")),
                       "goals":_si(row.get("goals")),
                       "total_xg":_sf(row.get("total_xg")),
                       "xg_per_shot":_sf(row.get("xg_per_shot"))},
        "positioning":{"avg_position_x":_sf(row.get("avg_position_x")),
                       "avg_position_y":_sf(row.get("avg_position_y")),
                       "position_deviation":_sf(row.get("position_deviation")),
                       "attacking_tendency":_sf(row.get("attacking_tendency"))},
        "pressing":   {"total_pressures":_si(row.get("total_pressures")),
                       "pressure_regains":_si(row.get("pressure_regains")),
                       "pressing_efficiency":_sf(row.get("pressing_efficiency"))},
        "physical":   {"total_actions":_si(row.get("total_actions")),
                       "distance_covered":_sf(row.get("distance_covered")),
                       "activity_drop_2nd_half":_sf(row.get("activity_drop_2nd_half"))},
        "behavioral": {"fouls_committed":_si(row.get("fouls_committed")),
                       "yellow_cards":_si(row.get("yellow_cards")),
                       "ball_retention_rate":_sf(row.get("ball_retention_rate"))},
    }


@router.get("/player/{player_id}/history")
def get_history(player_id: int, season_id: Optional[int] = Query(None)):
    d  = _load_data()
    ps = d["scores"][d["scores"]["player_id"] == player_id]
    if not len(ps): raise HTTPException(404, f"Player {player_id} not found")

    history = ps.merge(
        d["matches"][["match_id","match_date","home_team","away_team"]],
        on="match_id", how="left"
    ).sort_values("match_date")

    return {
        "player_id":   player_id,
        "player_name": str(ps.iloc[0]["player_name"]),
        "matches":     [{"match_id":_si(r["match_id"]),"match_date":str(r.get("match_date","")),
                         "home_team":str(r.get("home_team","")),"away_team":str(r.get("away_team","")),
                         "overall_score":_sf(r["overall_score"]),"vaep_rating":_sf(r.get("vaep_rating"))}
                        for _, r in history.iterrows()],
        "season_avg":  {"overall_score":_sf(ps["overall_score"].mean()),
                         "vaep_rating":_sf(ps["vaep_rating"].mean()) if "vaep_rating" in ps.columns else None},
    }


@router.get("/player/compare")
def compare(player_ids: str = Query(...), match_id: Optional[int] = Query(None)):
    d    = _load_data()
    # Handle float strings like "4320.0" from frontend
    ids  = [int(float(i.strip())) for i in player_ids.split(",")]
    result = []
    for pid in ids:
        ps = d["scores"][d["scores"]["player_id"] == pid]
        if not len(ps): continue
        row = ps[ps["match_id"]==match_id].iloc[0] if match_id and len(ps[ps["match_id"]==match_id]) \
              else ps.mean(numeric_only=True)
        result.append({
            "player_id":    pid,
            "player_name":  str(ps.iloc[0]["player_name"]),
            "position":     str(ps.iloc[0].get("position_group","Unknown")),
            "overall_score":_sf(row["overall_score"]),
            "scores": {"passing":_sf(row["passing_score"]),"shooting":_sf(row["shooting_score"]),
                       "positioning":_sf(row["positioning_score"]),"pressing":_sf(row["pressing_score"]),
                       "movement":_sf(row["movement_score"]),"physical":_sf(row["physical_score"]),
                       "behavioral":_sf(row["behavioral_score"])},
            "vaep_rating":_sf(row.get("vaep_rating")),
        })
    return {"comparison": result}


@router.get("/player/dashboard/{player_name}")
def get_dashboard(player_name: str, match_id: Optional[int] = Query(None)):
    """
    الـ Endpoint الرئيسي للـ Player Dashboard
    بيرجع كل الـ charts كـ base64 images
    """
    result = generate_all_charts(player_name, match_id=match_id)
    if "error" in result:
        raise HTTPException(404, result["error"])
    return result


@router.get("/player/dashboard-data/{player_name}")
def get_dashboard_data(player_name: str, match_id: Optional[int] = Query(None)):
    """
    Endpoint لرجع البيانات الخام للـ Charts (JSON)
    مناسب للـ Frontend Rendering مع Animation
    """
    result = get_player_chart_data(player_name, match_id=match_id)
    if "error" in result:
        raise HTTPException(404, result["error"])
    return result
