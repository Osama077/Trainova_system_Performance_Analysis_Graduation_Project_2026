import React, { useEffect, useState, useMemo } from 'react';
import { Loader2, Users, Search } from 'lucide-react';
import { SquadAPI } from '../api';
import { useAppContext } from '../context/AppContext';
import ErrorAlert from './ErrorAlert';

const POS_GROUP_MAP = { GK: 'GK', DF: 'Defender', MF: 'Midfielder', FW: 'Attacker' };
const POS_GROUP_TO_SHORT = { GK: 'GK', Defender: 'DF', Midfielder: 'MF', Attacker: 'FW' };

const POS_FILTERS = [
  { key: '', label: 'All' },
  { key: 'GK', label: 'GK' },
  { key: 'DF', label: 'DF' },
  { key: 'MF', label: 'MF' },
  { key: 'FW', label: 'FW' },
];

const POS_COLORS = {
  FW: { bg: 'rgba(248,81,73,0.15)', text: '#F85149', border: '#F8514944' },
  MF: { bg: 'rgba(0,208,132,0.15)', text: '#00D084', border: '#00D08444' },
  DF: { bg: 'rgba(88,166,255,0.15)', text: '#58A6FF', border: '#58A6FF44' },
  GK: { bg: 'rgba(168,85,247,0.15)', text: '#A855F7', border: '#A855F744' },
};

function scoreColor(v) {
  if (v == null) return 'text-slate-400';
  if (v >= 8) return 'text-purple-600';
  if (v >= 7) return 'text-violet-600';
  if (v >= 6) return 'text-indigo-600';
  if (v >= 5) return 'text-blue-600';
  return 'text-slate-500';
}

function scoreBg(v) {
  if (v == null) return 'bg-slate-100 text-slate-400';
  if (v >= 8) return 'bg-purple-100 text-purple-700';
  if (v >= 7) return 'bg-violet-100 text-violet-700';
  if (v >= 6) return 'bg-indigo-100 text-indigo-700';
  if (v >= 5) return 'bg-blue-100 text-blue-700';
  return 'bg-slate-100 text-slate-500';
}

function initials(name) {
  return (name || '??').split(' ').map(s => s[0]).filter(Boolean).slice(0, 2).join('').toUpperCase();
}

const PlayerProfiles = () => {
  const { selectedSeason, openPlayerDashboard } = useAppContext();
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [posFilter, setPosFilter] = useState('');
  const [search, setSearch] = useState('');

  const fetchData = async (season) => {
    setLoading(true);
    setError(null);
    try {
      const result = await SquadAPI.getSeasonPlayers(season);
      setData(result);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (selectedSeason) fetchData(selectedSeason);
  }, [selectedSeason]);

  const filteredPlayers = useMemo(() => {
    if (!data?.players) return [];
    return data.players.filter(p => {
      if (posFilter && p.position_group !== POS_GROUP_MAP[posFilter]) return false;
      if (search && !p.player_name?.toLowerCase().includes(search.toLowerCase())) return false;
      return true;
    });
  }, [data, posFilter, search]);

  if (loading) {
    return (
      <div className="surface flex items-center justify-center p-12">
        <Loader2 className="h-6 w-6 animate-spin text-brand-600" />
      </div>
    );
  }
  if (error) return <ErrorAlert message={error} onRetry={() => fetchData(selectedSeason)} />;
  if (!data) return <ErrorAlert message="No player data available" />;

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between">
        <div className="flex items-center gap-3">
          <Users className="h-5 w-5 text-brand-600" />
          <h2 className="text-lg font-bold text-slate-900">Player Profiles — {data.team}</h2>
          <span className="text-xs text-slate-400">{data.season}</span>
          <span className="rounded-full bg-slate-100 px-2 py-0.5 text-[10px] font-mono text-slate-500">{data.player_count} players</span>
        </div>
      </div>

      <div className="flex items-center gap-3">
        <div className="flex items-center gap-1.5 rounded-lg bg-slate-100 px-3 py-2">
          <Search className="h-3.5 w-3.5 text-slate-400" />
          <input
            type="text"
            placeholder="Search players..."
            value={search}
            onChange={e => setSearch(e.target.value)}
            className="bg-transparent text-xs text-slate-700 outline-none w-40 placeholder:text-slate-400"
          />
        </div>
        <div className="flex gap-1">
          {POS_FILTERS.map(f => (
            <button
              key={f.key}
              onClick={() => setPosFilter(f.key)}
              className={`px-3 py-1.5 rounded-lg text-xs font-semibold transition ${
                posFilter === f.key
                  ? 'bg-brand-600 text-white'
                  : 'bg-slate-100 text-slate-500 hover:bg-slate-200'
              }`}
            >
              {f.label}
            </button>
          ))}
        </div>
      </div>

      <div className="surface overflow-hidden">
        <div className="overflow-x-auto" style={{ scrollbarWidth: 'thin' }}>
          <table className="w-full text-xs">
            <thead>
              <tr className="border-b border-slate-200 bg-slate-50 text-[10px] font-bold text-slate-500 uppercase tracking-wider">
                <th className="px-3 py-2.5 text-left">Player</th>
                <th className="px-3 py-2.5 text-left">Pos</th>
                <th className="px-3 py-2.5 text-center">Score</th>
                <th className="px-3 py-2.5 text-center">Matches</th>
                <th className="px-3 py-2.5 text-center">VAEP</th>
                <th className="px-3 py-2.5 text-center">G/90</th>
                <th className="px-3 py-2.5 text-center">A/90</th>
                <th className="px-3 py-2.5 text-center">Pass%</th>
                <th className="px-3 py-2.5 text-center">S/90</th>
                <th className="px-3 py-2.5 text-center">Drb/90</th>
                <th className="px-3 py-2.5 text-center">Press/90</th>
                <th className="px-3 py-2.5 text-center">PrgP/90</th>
                <th className="px-3 py-2.5 text-center">xA/90</th>
              </tr>
            </thead>
            <tbody>
              {filteredPlayers.map(p => {
                const sc = POS_GROUP_TO_SHORT[p.position_group] || 'MF';
                const pc = POS_COLORS[sc] || POS_COLORS.MF;
                return (
                  <tr
                    key={p.player_id}
                    onClick={() => openPlayerDashboard(p.player_name, p.player_id, null)}
                    className="border-b border-slate-100 cursor-pointer transition hover:bg-slate-50"
                  >
                    <td className="px-3 py-2">
                      <div className="flex items-center gap-2">
                        <div
                          className="flex h-8 w-8 shrink-0 items-center justify-center rounded-full text-[9px] font-bold"
                          style={{ background: pc.bg, color: pc.text, border: `1.5px solid ${pc.border}` }}
                        >
                          {initials(p.player_name)}
                        </div>
                        <div className="min-w-0">
                          <div className="text-xs font-bold text-slate-800 truncate max-w-[200px]">{p.player_name}</div>
                          <div className="text-[9px] text-slate-500">{p.position_granular}</div>
                        </div>
                      </div>
                    </td>
                    <td className="px-3 py-2">
        <span
                          className="inline-block rounded px-1.5 py-0.5 text-[10px] font-bold"
                          style={{ background: pc.bg, color: pc.text }}
                        >
                          {sc}
                        </span>
                    </td>
                    <td className="px-3 py-2 text-center">
                      <span className={`inline-block rounded-full px-2 py-0.5 text-[11px] font-black font-mono ${scoreBg(p.avg_score)}`}>
                        {p.avg_score?.toFixed(1) || '—'}
                      </span>
                    </td>
                    <td className="px-3 py-2 text-center font-mono text-slate-600">{p.matches_played}</td>
                    <td className={`px-3 py-2 text-center font-mono font-bold ${p.avg_vaep_rating >= 1 ? 'text-emerald-600' : p.avg_vaep_rating >= 0.5 ? 'text-blue-600' : 'text-slate-500'}`}>
                      {p.avg_vaep_rating?.toFixed(2) || '—'}
                    </td>
                    <td className="px-3 py-2 text-center font-mono text-slate-700">{p.goals_per90?.toFixed(2) || '—'}</td>
                    <td className="px-3 py-2 text-center font-mono text-slate-700">{p.assists_per90?.toFixed(2) || '—'}</td>
                    <td className="px-3 py-2 text-center font-mono" style={{ color: p.pass_accuracy >= 85 ? '#22c55e' : p.pass_accuracy >= 75 ? '#3b82f6' : '#f59e0b' }}>
                      {p.pass_accuracy?.toFixed(1) || '—'}
                    </td>
                    <td className="px-3 py-2 text-center font-mono text-slate-700">{p.shots_per90?.toFixed(2) || '—'}</td>
                    <td className="px-3 py-2 text-center font-mono text-slate-700">{p.dribbles_per90?.toFixed(2) || '—'}</td>
                    <td className="px-3 py-2 text-center font-mono text-slate-700">{p.pressure_regains_per90?.toFixed(2) || '—'}</td>
                    <td className="px-3 py-2 text-center font-mono text-slate-700">{p.progressive_passes_per90?.toFixed(2) || '—'}</td>
                    <td className="px-3 py-2 text-center font-mono text-slate-700">{p.chances_created_per90?.toFixed(2) || '—'}</td>
                  </tr>
                );
              })}
            </tbody>
          </table>
          {filteredPlayers.length === 0 && (
            <div className="p-8 text-center text-sm text-slate-400">No players match the current filters.</div>
          )}
        </div>
      </div>
    </div>
  );
};

export default PlayerProfiles;