# Player Comparison Feature

Reusable player comparison module for the Trainova ML services area.

## What it does

- Lets analysts select up to five players.
- Calls `GET /api/v1/player/compare?player_ids=...` through the frontend API client.
- Renders radar and bar-based comparison views plus a metric table.

## Data expectations

- Player list payload: `player_items[]` with `player_id`, `player_name`, and `team_name`.
- Comparison payload: `comparison[]` entries with `overall_score`, `vaep_rating`, `position`, and metric scores.

## Notes

- The component is designed to stay lightweight and reusable.
- ML outputs and generated artifacts should be ignored in the sibling `.gitignore` file.