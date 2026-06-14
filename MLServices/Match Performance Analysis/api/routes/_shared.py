"""
api/routes/analysis.py
api/routes/player.py
api/routes/team.py
api/routes/match.py
api/routes/benchmark.py
"""

# ── Shared data loader ────────────────────────────────────────────────────────
import json
import numpy as np
import pandas as pd
from pathlib import Path
from functools import lru_cache
from config import DATA_DIR, MODELS_DIR, SEASONS_LIST

GRANULAR_POSITIONS = [
    "Goalkeeper", "Center Back", "Full Back",
    "Defensive Midfielder", "Central Midfielder", "Attacking Midfielder",
    "Winger", "Striker",
]

GRANULAR_LABELS = {
    "Goalkeeper": "GK", "Center Back": "CB", "Full Back": "FB",
    "Defensive Midfielder": "DMF", "Central Midfielder": "CMF",
    "Attacking Midfielder": "AMF", "Winger": "WG", "Striker": "ST",
}

SEASONS_DIR = DATA_DIR / "seasons"


def _merge_score_data(sc: pd.DataFrame, kpi: pd.DataFrame) -> pd.DataFrame:
    """Merge score data (from position_scores) into main scores dataframe.
    Adds score, score_label, position_granular, confidence as separate fields.
    For backward compatibility, also handles old 'position_kpi' column name."""
    sc = sc.copy()
    if kpi is None or not len(kpi):
        return sc
    # Handle both old (position_kpi) and new (score) column names
    score_col = "score" if "score" in kpi.columns else ("position_kpi" if "position_kpi" in kpi.columns else None)
    label_col = "score_label" if "score_label" in kpi.columns else ("position_kpi_label" if "position_kpi_label" in kpi.columns else None)
    if score_col is None:
        return sc
    merge_cols = ["match_id", "player_id", score_col, "position_granular", "confidence"]
    if label_col:
        merge_cols.append(label_col)
    # Add any kpi_ or score_ prefix dimension columns
    dim_cols = [c for c in kpi.columns if c.startswith("kpi_") or c.startswith("score_")]
    merge_cols.extend(dim_cols)
    avail = [c for c in merge_cols if c in kpi.columns]
    sc = sc.merge(kpi[avail], on=["match_id", "player_id"], how="left")
    # Normalize column name: ensure it's called 'score'
    if score_col != "score":
        sc = sc.rename(columns={score_col: "score"})
    if label_col and label_col != "score_label":
        sc = sc.rename(columns={label_col: "score_label"})
    # Normalize dimension columns: kpi_* → score_*
    kpi_cols = [c for c in sc.columns if c.startswith("kpi_")]
    renames = {c: "score_" + c[4:] for c in kpi_cols}
    sc = sc.rename(columns=renames)
    if "score_label" not in sc.columns:
        sc["score_label"] = ""
    if "score" not in sc.columns:
        sc["score"] = None
    # Fill granular position
    if "position_granular" in sc.columns:
        sc["position_granular"] = sc["position_granular"].fillna(
            sc.get("position_group", "").map(
                {"GK": "Goalkeeper", "Defender": "Center Back",
                 "Midfielder": "Central Midfielder", "Attacker": "Winger"}
            ).fillna("Central Midfielder")
        )
    return sc


_load_data_cache = {"data": None, "mtime_key": ""}

def _load_data():
    """Load combined data from all seasons (single parquet files).
    Cached with automatic invalidation when source files change."""
    # Try new filename first, fall back to old
    score_path = DATA_DIR / "position_scores.parquet"
    kpi_path = DATA_DIR / "position_kpi.parquet"
    score_path_actual = score_path if score_path.exists() else (kpi_path if kpi_path.exists() else None)
    data_files = [
        "events_clean.parquet", "computed_features.parquet", "model_scores.parquet",
        "player_vaep_ratings.parquet", "matches.parquet", "lineups.parquet",
        "position_benchmarks.parquet",
    ]
    mtime_parts = []
    for f in data_files:
        fp = DATA_DIR / f
        if fp.exists():
            mtime_parts.append(str(fp.stat().st_mtime))
    if score_path_actual:
        mtime_parts.append(str(score_path_actual.stat().st_mtime))
    weights_path = MODELS_DIR / "position_weights.json"
    if weights_path.exists():
        mtime_parts.append(str(weights_path.stat().st_mtime))
    mtime_key = "|".join(mtime_parts)

    if _load_data_cache["data"] is not None and _load_data_cache["mtime_key"] == mtime_key:
        return _load_data_cache["data"]

    d = {
        "events":    pd.read_parquet(DATA_DIR / "events_clean.parquet"),
        "computed":  pd.read_parquet(DATA_DIR / "computed_features.parquet"),
        "scores":    pd.read_parquet(DATA_DIR / "model_scores.parquet"),
        "vaep":      pd.read_parquet(DATA_DIR / "player_vaep_ratings.parquet"),
        "matches":   pd.read_parquet(DATA_DIR / "matches.parquet"),
        "lineups":   pd.read_parquet(DATA_DIR / "lineups.parquet"),
        "bench":     pd.read_parquet(DATA_DIR / "position_benchmarks.parquet"),
        "weights":   json.loads((MODELS_DIR / "position_weights.json").read_text(encoding="utf-8")),
        "position_scores": pd.read_parquet(score_path_actual) if score_path_actual else pd.DataFrame(),
    }
    d["scores"] = _merge_score_data(d["scores"], d["position_scores"])
    _load_data_cache["data"] = d
    _load_data_cache["mtime_key"] = mtime_key
    return d


def _load_season(season_label: str) -> dict:
    """Load a single season from per-season parquet files.
    Falls back to filtering combined data if per-season files don't exist."""
    season_dir = SEASONS_DIR / season_label.replace("/", "_")

    def _try_read(path, fallback_key=None, filter_col="season_label"):
        if path.exists():
            return pd.read_parquet(path)
        if fallback_key:
            combined = _load_data()[fallback_key]
            if filter_col and filter_col in combined.columns:
                return combined[combined[filter_col] == season_label].copy()
            return combined.copy()
        return pd.DataFrame()

    result = {
        "events":    _try_read(season_dir / "events_clean.parquet",          "events"),
        "computed":  _try_read(season_dir / "computed_features.parquet",     "computed"),
        "scores":    _try_read(season_dir / "model_scores.parquet",          "scores"),
        "vaep":      _try_read(season_dir / "player_vaep_ratings.parquet",   "vaep"),
        "matches":   _try_read(season_dir / "matches.parquet",               "matches"),
        "lineups":   _try_read(season_dir / "lineups.parquet",               "lineups"),
        "bench":     _try_read(season_dir / "position_benchmarks.parquet",   "bench", filter_col=None),
        "weights":   json.loads((MODELS_DIR / "position_weights.json").read_text(encoding="utf-8")),
    }
    # Include position_scores filtered by this season's match IDs
    sc_all = _load_data().get("position_scores", pd.DataFrame())
    if len(sc_all):
        scores_season = result.get("scores")
        if scores_season is not None and "match_id" in scores_season.columns:
            season_mids = scores_season["match_id"].unique()
            result["position_scores"] = sc_all[sc_all["match_id"].isin(season_mids)]
        else:
            result["position_scores"] = sc_all
    else:
        result["position_scores"] = pd.DataFrame()
    if "score" not in result.get("scores", pd.DataFrame()).columns:
        result["scores"] = _merge_score_data(result["scores"], result.get("position_scores", pd.DataFrame()))
    return result


@lru_cache(maxsize=32)
def _load_season_cached(season_label: str):
    """Cached wrapper for _load_season."""
    return _load_season(season_label)


def _load(season: str = None):
    """
    Load data for a specific season or all seasons combined.
    Season format expected: "YYYY/YYYY" (e.g. "2015/2016").
    If season is None, returns combined data (all seasons).
    """
    if season is not None:
        if not isinstance(season, str) or "/" not in season:
            from fastapi import HTTPException
            raise HTTPException(400, f"Invalid season format '{season}'. Expected format: YYYY/YYYY (e.g. 2015/2016)")
        return _load_season_cached(season)
    return _load_data()


def _get_available_seasons():
    """Return list of available seasons from config."""
    return [{"competition_id": c, "season_id": s, "label": l} for c, s, l in SEASONS_LIST]


def _normalize_score_columns(df: pd.DataFrame) -> pd.DataFrame:
    """Normalize score DataFrame columns from old (position_kpi/kpi_*) to new (score/score_*) format."""
    df = df.copy()
    if "position_kpi" in df.columns and "score" not in df.columns:
        df = df.rename(columns={"position_kpi": "score"})
    if "position_kpi_label" in df.columns and "score_label" not in df.columns:
        df = df.rename(columns={"position_kpi_label": "score_label"})
    for c in list(df.columns):
        if c.startswith("kpi_"):
            df = df.rename(columns={c: "score_" + c[4:]})
    return df


def _sf(v):
    if v is None or (isinstance(v, float) and np.isnan(v)): return None
    return round(float(v), 4)

def _si(v):
    if v is None or (isinstance(v, float) and np.isnan(v)): return 0
    return int(v)

def _to_records(df):
    return json.loads(df.to_json(orient="records", default_handler=str))


CLUSTER_SHORT = {
    "Creative Playmaker": "creator",
    "Box-to-Box Midfielder": "engine",
    "Target Forward": "dribbler",
    "Ball-Playing Defender": "stopper",
    "Pressing Machine": "presser",
}


def _initials(name):
    parts = name.split()
    if len(parts) >= 2:
        return (parts[0][0] + parts[-1][0]).upper()
    return name[:2].upper()
