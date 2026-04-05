"""api/routes/benchmark.py"""
from fastapi import APIRouter, HTTPException
from api.routes._shared import _load_data, _sf

router = APIRouter()

VALID = ["Attacker", "Midfielder", "Defender", "GK"]

@router.get("/benchmark/{position_group}")
def get_benchmark(position_group: str):
    if position_group not in VALID:
        raise HTTPException(400, f"Invalid position. Choose from: {VALID}")
    d       = _load_data()
    weights = d["weights"].get(position_group, d["weights"]["Midfielder"])
    bench   = d["bench"]
    avgs    = {}
    if position_group in bench.index:
        row = bench.loc[position_group]
        avgs = {col: _sf(row[col]) for col in bench.columns}
    return {"position_group": position_group, "averages": avgs, "weights": weights}
