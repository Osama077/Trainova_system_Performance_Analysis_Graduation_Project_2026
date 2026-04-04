import React, { useEffect, useMemo, useState } from 'react';
import {
  Bar,
  BarChart,
  CartesianGrid,
  Cell,
  Legend,
  PolarAngleAxis,
  PolarGrid,
  PolarRadiusAxis,
  Radar,
  RadarChart,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
} from 'recharts';
import { GitCompareArrows, Minus, Plus, RotateCcw, Search, ShieldCheck, Users } from 'lucide-react';

const API_BASE_URL = 'http://localhost:8000/api/v1';

const MAX_SELECTION = 5;

const METRICS = [
  { key: 'passing', label: 'Passing' },
  { key: 'shooting', label: 'Shooting' },
  { key: 'positioning', label: 'Positioning' },
  { key: 'pressing', label: 'Pressing' },
  { key: 'movement', label: 'Movement' },
  { key: 'physical', label: 'Physical' },
  { key: 'behavioral', label: 'Behavioral' },
];

const COLORS = ['#0f766e', '#2563eb', '#dc2626', '#f59e0b', '#7c3aed'];

const metricValue = (player, metric) => Number(player?.scores?.[metric] ?? 0);

const scoreColor = (value) => {
  if (value >= 8) return 'text-emerald-700';
  if (value >= 6) return 'text-brand-700';
  if (value >= 4) return 'text-amber-700';
  return 'text-rose-700';
};

const scoreBarClass = (value) => {
  if (value >= 8) return 'from-emerald-500 to-teal-500';
  if (value >= 6) return 'from-brand-500 to-cyan-500';
  if (value >= 4) return 'from-amber-400 to-orange-500';
  return 'from-rose-400 to-rose-600';
};

const LoadingState = ({ message }) => (
  <div className="surface p-10 text-center" role="status" aria-live="polite">
    <p className="text-sm font-medium text-slate-700">{message}</p>
  </div>
);

const ErrorState = ({ message, onDismiss }) => (
  <div className="surface border-rose-200 bg-rose-50 p-4 text-rose-900" role="alert" aria-live="assertive">
    <div className="flex items-start justify-between gap-3">
      <div>
        <p className="font-semibold">Error</p>
        <p className="mt-1 text-sm">{message}</p>
      </div>
      {onDismiss ? (
        <button
          type="button"
          onClick={onDismiss}
          className="rounded-md border border-rose-200 px-2 py-1 text-xs font-semibold text-rose-700 transition hover:bg-rose-100"
        >
          Dismiss
        </button>
      ) : null}
    </div>
  </div>
);

const fetchJson = async (path, options = {}) => {
  const response = await fetch(`${API_BASE_URL}${path}`, {
    headers: {
      'Content-Type': 'application/json',
      ...(options.headers || {}),
    },
    ...options,
  });

  if (!response.ok) {
    let detail = `Request failed with status ${response.status}`;
    try {
      const payload = await response.json();
      detail = payload?.detail || detail;
    } catch {
      // Ignore JSON parse errors and fall back to the default message.
    }
    throw new Error(detail);
  }

  return response.json();
};

const PlayerComparison = () => {
  const [playerItems, setPlayerItems] = useState([]);
  const [selectedPlayerIds, setSelectedPlayerIds] = useState([]);
  const [comparison, setComparison] = useState([]);
  const [loading, setLoading] = useState(true);
  const [loadingComparison, setLoadingComparison] = useState(false);
  const [error, setError] = useState(null);
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedTeam, setSelectedTeam] = useState('all');

  useEffect(() => {
    const fetchPlayers = async () => {
      try {
        setLoading(true);
        setError(null);
        const response = await fetchJson('/player/list');
        setPlayerItems(response.player_items || []);
      } catch (err) {
        setError(err.message || 'Failed to load players for comparison.');
      } finally {
        setLoading(false);
      }
    };

    fetchPlayers();
  }, []);

  const teamOptions = useMemo(() => {
    const teams = playerItems.map((item) => item.team_name || 'Unknown');
    return ['all', ...Array.from(new Set(teams)).sort((a, b) => a.localeCompare(b))];
  }, [playerItems]);

  const filteredPlayers = useMemo(
    () =>
      playerItems.filter((item) => {
        const nameMatch = String(item.player_name || '')
          .toLowerCase()
          .includes(searchTerm.toLowerCase());
        const teamMatch = selectedTeam === 'all' || (item.team_name || 'Unknown') === selectedTeam;
        return nameMatch && teamMatch;
      }),
    [playerItems, searchTerm, selectedTeam]
  );

  const selectedPlayers = useMemo(
    () =>
      selectedPlayerIds
        .map((playerId) => playerItems.find((item) => String(item.player_id) === String(playerId)))
        .filter(Boolean),
    [playerItems, selectedPlayerIds]
  );

  const radarData = useMemo(
    () =>
      METRICS.map((metric) => {
        const row = { metric: metric.label };
        comparison.forEach((player, index) => {
          row[`player_${index}`] = metricValue(player, metric.key);
        });
        return row;
      }),
    [comparison]
  );

  const compareData = useMemo(
    () =>
      comparison.map((player) => ({
        name: player.player_name,
        overall: Number(player.overall_score ?? 0),
      })),
    [comparison]
  );

  const summary = useMemo(() => {
    if (!comparison.length) {
      return {
        bestOverall: null,
        bestVAEP: null,
        avgOverall: null,
      };
    }

    const sortedByOverall = [...comparison].sort((a, b) => Number(b.overall_score ?? 0) - Number(a.overall_score ?? 0));
    const sortedByVaep = [...comparison].sort((a, b) => Number(b.vaep_rating ?? 0) - Number(a.vaep_rating ?? 0));
    const avgOverall = comparison.reduce((sum, player) => sum + Number(player.overall_score ?? 0), 0) / comparison.length;

    return {
      bestOverall: sortedByOverall[0] || null,
      bestVAEP: sortedByVaep[0] || null,
      avgOverall,
    };
  }, [comparison]);

  const togglePlayer = (player) => {
    const playerId = String(player.player_id);

    setSelectedPlayerIds((current) => {
      if (current.includes(playerId)) {
        return current.filter((id) => id !== playerId);
      }

      if (current.length >= MAX_SELECTION) {
        return current;
      }

      return [...current, playerId];
    });
  };

  const handleCompare = async () => {
    if (selectedPlayerIds.length < 2) {
      setError('Select at least two players before running a comparison.');
      return;
    }

    try {
      setLoadingComparison(true);
      setError(null);
      const response = await fetchJson(`/player/compare?player_ids=${selectedPlayerIds.map((id) => Number(id)).join(',')}`);
      setComparison(response.comparison || []);
    } catch (err) {
      setComparison([]);
      setError(err.message || 'Failed to compare players.');
    } finally {
      setLoadingComparison(false);
    }
  };

  const clearSelection = () => {
    setSelectedPlayerIds([]);
    setComparison([]);
    setSearchTerm('');
    setSelectedTeam('all');
    setError(null);
  };

  if (loading) {
    return <LoadingState message="Loading players for comparison..." />;
  }

  return (
    <div className="space-y-4">
      {error ? <ErrorState message={error} onDismiss={() => setError(null)} /> : null}

      <div className="surface p-6 sm:p-8">
        <div className="flex flex-col gap-6 xl:flex-row xl:items-start xl:justify-between">
          <div className="max-w-3xl">
            <div className="section-header section-header-comparison">
              <div className="flex items-center gap-2">
                <GitCompareArrows className="h-5 w-5" />
                <h2 className="text-2xl font-semibold sm:text-3xl">Player Comparison Studio</h2>
              </div>
              <p className="mt-1 text-sm text-white/90">
                Select up to {MAX_SELECTION} players, compare season-level outputs, and inspect where each profile leads.
              </p>
            </div>

            <div className="grid grid-cols-1 gap-3 sm:grid-cols-3">
              <div className="metric-card metric-active">
                <p className="text-xs uppercase tracking-wide text-slate-500">Selected players</p>
                <p className="mt-1 text-2xl font-semibold text-slate-900">{selectedPlayerIds.length}</p>
              </div>
              <div className="metric-card metric-active">
                <p className="text-xs uppercase tracking-wide text-slate-500">Compared rows</p>
                <p className="mt-1 text-2xl font-semibold text-slate-900">{comparison.length}</p>
              </div>
              <div className="metric-card">
                <p className="text-xs uppercase tracking-wide text-slate-500">Mode</p>
                <p className="mt-1 text-base font-semibold text-slate-900">Season aggregate comparison</p>
              </div>
            </div>
          </div>

          <div className="surface-muted w-full max-w-xl p-4">
            <div className="flex items-start gap-3">
              <div className="rounded-lg bg-slate-900 p-2 text-white">
                <ShieldCheck className="h-4 w-4" />
              </div>
              <div>
                <p className="text-sm font-semibold text-slate-900">Selection rules</p>
                <p className="mt-1 text-sm text-slate-600">
                  Compare at least two players, remove a chip to swap selections, and press Compare to fetch the backend ranking payload.
                </p>
              </div>
            </div>
            {selectedPlayers.length > 0 ? (
              <div className="mt-4 flex flex-wrap gap-2">
                {selectedPlayers.map((player) => (
                  <span key={player.player_id} className="color-chip bg-white/90">
                    {player.player_name}
                  </span>
                ))}
              </div>
            ) : null}
          </div>
        </div>
      </div>

      <section className="surface p-6">
        <div className="mb-4 flex flex-col gap-3 lg:flex-row lg:items-center lg:justify-between">
          <div>
            <h3 className="text-lg font-semibold text-slate-900">Select players</h3>
            <p className="text-sm text-slate-600">Search by name or team, then toggle up to {MAX_SELECTION} players.</p>
          </div>
          <div className="flex flex-wrap gap-2">
            <button type="button" className="btn-primary" onClick={handleCompare} disabled={selectedPlayerIds.length < 2 || loadingComparison}>
              {loadingComparison ? 'Comparing...' : 'Compare players'}
            </button>
            <button type="button" className="btn-secondary" onClick={clearSelection}>
              <RotateCcw className="mr-2 h-4 w-4" />
              Clear
            </button>
          </div>
        </div>

        <div className="grid gap-3 md:grid-cols-[minmax(0,1fr)_220px]">
          <div className="relative">
            <Search className="pointer-events-none absolute left-3 top-2.5 h-4 w-4 text-slate-400" />
            <input
              type="search"
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              className="field pl-9"
              placeholder="Search players"
              aria-label="Search players by name"
            />
          </div>
          <select
            value={selectedTeam}
            onChange={(e) => setSelectedTeam(e.target.value)}
            className="field"
            aria-label="Filter players by team"
          >
            {teamOptions.map((team) => (
              <option key={team} value={team}>
                {team === 'all' ? 'All teams' : team}
              </option>
            ))}
          </select>
        </div>

        <div className="mt-4 flex flex-wrap gap-2">
          {selectedPlayers.map((player) => (
            <button
              key={player.player_id}
              type="button"
              onClick={() => togglePlayer(player)}
              className="inline-flex items-center gap-2 rounded-full border border-brand-200 bg-brand-50 px-3 py-1.5 text-sm font-medium text-brand-800"
            >
              {player.player_name}
              <Minus className="h-3.5 w-3.5" />
            </button>
          ))}
          {selectedPlayers.length === 0 ? (
            <span className="text-sm text-slate-500">No players selected yet.</span>
          ) : null}
        </div>

        <div className="mt-6 grid grid-cols-1 gap-3 md:grid-cols-2 xl:grid-cols-3">
          {filteredPlayers.map((player) => {
            const isSelected = selectedPlayerIds.includes(String(player.player_id));

            return (
              <button
                key={`${player.player_id}-${player.player_name}`}
                type="button"
                onClick={() => togglePlayer(player)}
                className={`surface group p-4 text-left transition hover:-translate-y-0.5 hover:shadow-xl focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-brand-500 ${
                  isSelected ? 'metric-active border-brand-300' : ''
                }`}
              >
                <div className="flex items-start justify-between gap-3">
                  <div>
                    <p className="text-sm font-semibold text-slate-900">{player.player_name}</p>
                    <p className="mt-1 text-xs text-slate-500">{player.team_name || 'Unknown team'}</p>
                  </div>
                  <div className={`rounded-full p-2 ${isSelected ? 'bg-brand-600 text-white' : 'bg-slate-100 text-slate-500'}`}>
                    {isSelected ? <Minus className="h-4 w-4" /> : <Plus className="h-4 w-4" />}
                  </div>
                </div>
                <p className="mt-3 text-xs font-medium uppercase tracking-wide text-slate-500">Player ID {player.player_id}</p>
                <p className="mt-2 inline-flex items-center gap-2 text-xs font-medium text-brand-700">
                  {isSelected ? 'Selected' : 'Add to compare'}
                  <Users className="h-3.5 w-3.5" />
                </p>
              </button>
            );
          })}
        </div>
      </section>

      {comparison.length > 0 ? (
        <>
          <section className="surface p-6">
            <div className="mb-4 flex flex-col gap-2 lg:flex-row lg:items-center lg:justify-between">
              <div>
                <h3 className="text-lg font-semibold text-slate-900">Comparison summary</h3>
                <p className="text-sm text-slate-600">Backend ranking payload with overall score, VAEP, and metric breakdowns.</p>
              </div>
              <div className="flex flex-wrap gap-2 text-sm">
                <span className="color-chip">Average overall: {summary.avgOverall !== null ? summary.avgOverall.toFixed(2) : 'N/A'}</span>
                <span className="color-chip">Best overall: {summary.bestOverall ? summary.bestOverall.player_name : 'N/A'}</span>
                <span className="color-chip">Best VAEP: {summary.bestVAEP ? summary.bestVAEP.player_name : 'N/A'}</span>
              </div>
            </div>

            <div className="grid gap-6 xl:grid-cols-[1.1fr_0.9fr]">
              <div className="chart-frame p-4">
                <div className="mb-3 flex items-center justify-between gap-3">
                  <h4 className="text-sm font-semibold text-slate-800">Radar comparison</h4>
                  <div className="inline-flex items-center gap-1 text-xs text-slate-500">
                    <Users className="h-3.5 w-3.5" />
                    0 - 10 scale
                  </div>
                </div>
                <div className="h-[380px] w-full">
                  <ResponsiveContainer width="100%" height="100%">
                    <RadarChart data={radarData}>
                      <PolarGrid stroke="#dbeafe" />
                      <PolarAngleAxis dataKey="metric" tick={{ fill: '#475569', fontSize: 12 }} />
                      <PolarRadiusAxis domain={[0, 10]} angle={90} tick={{ fill: '#64748b', fontSize: 10 }} />
                      <Tooltip />
                      <Legend />
                      {comparison.map((player, index) => (
                        <Radar
                          key={player.player_id}
                          name={player.player_name}
                          dataKey={`player_${index}`}
                          stroke={COLORS[index % COLORS.length]}
                          fill={COLORS[index % COLORS.length]}
                          fillOpacity={0.16}
                        />
                      ))}
                    </RadarChart>
                  </ResponsiveContainer>
                </div>
              </div>

              <div className="chart-frame p-4">
                <div className="mb-3 flex items-center justify-between gap-3">
                  <h4 className="text-sm font-semibold text-slate-800">Overall score ranking</h4>
                  <GitCompareArrows className="h-4 w-4 text-slate-500" />
                </div>
                <div className="h-[380px] w-full">
                  <ResponsiveContainer width="100%" height="100%">
                    <BarChart data={compareData} layout="vertical" margin={{ top: 8, right: 12, left: 16, bottom: 8 }}>
                      <CartesianGrid strokeDasharray="3 3" stroke="#dbeafe" />
                      <XAxis type="number" domain={[0, 10]} tick={{ fill: '#475569', fontSize: 12 }} />
                      <YAxis type="category" dataKey="name" width={120} tick={{ fill: '#475569', fontSize: 12 }} />
                      <Tooltip />
                      <Bar dataKey="overall" radius={[0, 12, 12, 0]}>
                        {compareData.map((entry, index) => (
                          <Cell key={entry.name} fill={COLORS[index % COLORS.length]} />
                        ))}
                      </Bar>
                    </BarChart>
                  </ResponsiveContainer>
                </div>
              </div>
            </div>
          </section>

          <section className="surface p-6">
            <h3 className="mb-4 text-lg font-semibold text-slate-900">Metric breakdown</h3>
            <div className="overflow-x-auto">
              <table className="min-w-full border-separate border-spacing-y-3 text-sm">
                <thead>
                  <tr className="text-left text-xs uppercase tracking-wide text-slate-500">
                    <th className="px-3 py-2">Player</th>
                    <th className="px-3 py-2">Position</th>
                    <th className="px-3 py-2">Overall</th>
                    <th className="px-3 py-2">VAEP</th>
                    {METRICS.map((metric) => (
                      <th key={metric.key} className="px-3 py-2">
                        {metric.label}
                      </th>
                    ))}
                  </tr>
                </thead>
                <tbody>
                  {comparison.map((player) => (
                    <tr key={player.player_id} className="rounded-2xl bg-white/80 shadow-sm">
                      <td className="rounded-l-2xl px-3 py-3 font-semibold text-slate-900">{player.player_name}</td>
                      <td className="px-3 py-3 text-slate-600">{player.position || 'Unknown'}</td>
                      <td className={`px-3 py-3 font-semibold ${scoreColor(Number(player.overall_score ?? 0))}`}>
                        {Number(player.overall_score ?? 0).toFixed(2)}
                      </td>
                      <td className="px-3 py-3 text-slate-700">{Number(player.vaep_rating ?? 0).toFixed(2)}</td>
                      {METRICS.map((metric) => {
                        const value = metricValue(player, metric.key);
                        return (
                          <td key={metric.key} className="px-3 py-3 align-top">
                            <div className="flex w-28 items-center gap-2">
                              <div className="h-2 flex-1 rounded-full bg-slate-100">
                                <div
                                  className={`h-2 rounded-full bg-gradient-to-r ${scoreBarClass(value)}`}
                                  style={{ width: `${Math.max(0, Math.min(100, value * 10))}%` }}
                                />
                              </div>
                              <span className="w-10 text-right text-xs font-semibold text-slate-600">{value.toFixed(2)}</span>
                            </div>
                          </td>
                        );
                      })}
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </section>
        </>
      ) : (
        <section className="surface p-8 text-center">
          <p className="text-sm text-slate-600">
            Select at least two players and run the comparison to view the side-by-side breakdown.
          </p>
        </section>
      )}
    </div>
  );
};

export default PlayerComparison;