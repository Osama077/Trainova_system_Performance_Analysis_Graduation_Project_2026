import React, { useEffect, useMemo, useRef, useState } from 'react';
import { Activity, Download } from 'lucide-react';
import {
  Area,
  AreaChart,
  Bar,
  BarChart,
  CartesianGrid,
  Legend,
  Line,
  LineChart,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
} from 'recharts';
import { PlayerAPI } from '../api';
import LoadingSpinner from './LoadingSpinner';
import ErrorAlert from './ErrorAlert';

const PlayerAnimatedAnalysis = ({ playerName }) => {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [historySeries, setHistorySeries] = useState([]);
  const [dashboardData, setDashboardData] = useState(null);
  const [historyWindow, setHistoryWindow] = useState(10);
  const [exportMessage, setExportMessage] = useState('');

  const overallChartRef = useRef(null);
  const vaepChartRef = useRef(null);

  useEffect(() => {
    const fetchData = async () => {
      if (!playerName) return;
      try {
        setLoading(true);
        setError(null);

        const listResponse = await PlayerAPI.getPlayerList();
        const matched = (listResponse.player_items || []).find(
          (item) => String(item.player_name).toLowerCase() === String(playerName).toLowerCase()
        );

        if (!matched?.player_id) {
          setHistorySeries([]);
          setDashboardData(null);
          return;
        }

        const [historyResponse, dashboardResponse] = await Promise.all([
          PlayerAPI.getPlayerHistory(Number(matched.player_id)),
          PlayerAPI.getPlayerDashboardData(playerName),
        ]);

        const matches = historyResponse.matches || [];
        const normalized = matches
          .map((m, idx) => {
            const date = String(m.match_date || 'Unknown');
            const shortDate = date !== 'Unknown' ? date.slice(5, 10) : `M${idx + 1}`;
            return {
              id: m.match_id,
              label: shortDate,
              fullDate: date,
              overall: Number(m.overall_score ?? 0),
              vaep: Number(m.vaep_rating ?? 0),
            };
          })
          .slice(-20);

        setHistorySeries(normalized);
        setDashboardData(dashboardResponse || null);
      } catch (err) {
        setError(err.message || 'Failed to load animated analysis.');
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [playerName]);

  const historySeriesFiltered = useMemo(
    () => historySeries.slice(-historyWindow),
    [historySeries, historyWindow]
  );

  const historySummary = useMemo(() => {
    if (!historySeriesFiltered.length) {
      return { bestOverall: null, avgVaep: null };
    }
    const bestOverall = Math.max(...historySeriesFiltered.map((d) => d.overall));
    const avgVaep = historySeriesFiltered.reduce((acc, d) => acc + d.vaep, 0) / historySeriesFiltered.length;
    return { bestOverall, avgVaep };
  }, [historySeriesFiltered]);

  const downloadFromUrl = (url, filename) => {
    const link = document.createElement('a');
    link.href = url;
    link.download = filename;
    document.body.appendChild(link);
    link.click();
    link.remove();
  };

  const exportChart = async (containerRef, format, baseName) => {
    try {
      setExportMessage('');
      const container = containerRef.current;
      const svgElement = container?.querySelector('svg');
      if (!svgElement) {
        setExportMessage('No chart available to export.');
        return;
      }

      const serializer = new XMLSerializer();
      const svgString = serializer.serializeToString(svgElement);
      const svgBlob = new Blob([svgString], { type: 'image/svg+xml;charset=utf-8' });

      if (format === 'svg') {
        const svgUrl = URL.createObjectURL(svgBlob);
        downloadFromUrl(svgUrl, `${baseName}.svg`);
        URL.revokeObjectURL(svgUrl);
        setExportMessage(`${baseName}.svg exported.`);
        return;
      }

      const canvas = document.createElement('canvas');
      const bounds = svgElement.getBoundingClientRect();
      canvas.width = Math.max(900, Math.floor(bounds.width || 900));
      canvas.height = Math.max(520, Math.floor(bounds.height || 520));
      const ctx = canvas.getContext('2d');
      if (!ctx) {
        setExportMessage('PNG export failed: canvas context unavailable.');
        return;
      }

      const svgUrl = URL.createObjectURL(svgBlob);
      const img = new Image();
      img.onload = () => {
        ctx.fillStyle = '#ffffff';
        ctx.fillRect(0, 0, canvas.width, canvas.height);
        ctx.drawImage(img, 0, 0, canvas.width, canvas.height);
        URL.revokeObjectURL(svgUrl);

        const pngUrl = canvas.toDataURL('image/png');
        downloadFromUrl(pngUrl, `${baseName}.png`);
        setExportMessage(`${baseName}.png exported.`);
      };
      img.onerror = () => {
        URL.revokeObjectURL(svgUrl);
        setExportMessage('PNG export failed while rendering image.');
      };
      img.src = svgUrl;
    } catch (err) {
      setExportMessage('Chart export failed.');
    }
  };

  if (loading) return <LoadingSpinner message={`Loading animated analysis for ${playerName}...`} />;
  if (error) return <ErrorAlert message={error} />;

  const trendData = dashboardData?.charts?.trend || [];
  const radarData = dashboardData?.charts?.radar || { labels: [], values: [] };
  const breakdownData = dashboardData?.charts?.breakdown || [];
  const vaepData = dashboardData?.charts?.vaep || { timeline: [], totals: { offensive: 0, defensive: 0 } };
  const positionData = dashboardData?.charts?.position_comparison || [];
  const percentiles = dashboardData?.charts?.percentiles || {
    in_team: 0,
    in_league: 0,
    in_position: 0,
  };

  const radarChartData = radarData.labels.map((label, idx) => ({
    name: label,
    value: Number(radarData.values[idx] || 0),
  }));

  const percentileChartData = [
    { name: 'In Team', value: Number(percentiles.in_team || 0) },
    { name: 'In League', value: Number(percentiles.in_league || 0) },
    { name: 'In Position', value: Number(percentiles.in_position || 0) },
  ];

  return (
    <div className="space-y-4">
      <div className="theme-animated">
        <section className="surface p-6 sm:p-8">
          <div className="section-header section-header-animated mb-4 flex items-center gap-2">
            <Activity className="h-5 w-5" />
            <h2 className="text-2xl font-semibold">Animated Analysis - {playerName}</h2>
          </div>

          <div className="mb-4 flex flex-col gap-3 lg:flex-row lg:items-center lg:justify-between">
            <div className="inline-flex items-center gap-2 rounded-xl border border-slate-200 bg-white/80 px-3 py-2">
              <span className="text-sm text-slate-600">Recent matches</span>
              {[5, 10, 20].map((window) => (
                <button
                  key={window}
                  type="button"
                  onClick={() => setHistoryWindow(window)}
                  className={`rounded-md px-2.5 py-1 text-xs font-semibold transition ${
                    historyWindow === window
                      ? 'bg-brand-600 text-white'
                      : 'bg-slate-100 text-slate-700 hover:bg-slate-200'
                  }`}
                >
                  {window}
                </button>
              ))}
            </div>
          </div>

          {exportMessage ? (
            <div className="mb-4 rounded-lg border border-emerald-200 bg-emerald-50 px-3 py-2 text-xs text-emerald-800">
              {exportMessage}
            </div>
          ) : null}

          <div className="mb-4 grid grid-cols-1 gap-3 sm:grid-cols-2">
            <div className="metric-card metric-active">
              <p className="text-xs uppercase tracking-wide text-slate-500">Best overall (recent)</p>
              <p className="mt-1 text-2xl font-semibold text-brand-700">
                {historySummary.bestOverall !== null ? historySummary.bestOverall.toFixed(2) : 'N/A'}
              </p>
            </div>
            <div className="metric-card metric-active">
              <p className="text-xs uppercase tracking-wide text-slate-500">Average VAEP (recent)</p>
              <p className="mt-1 text-2xl font-semibold text-emerald-700">
                {historySummary.avgVaep !== null ? historySummary.avgVaep.toFixed(2) : 'N/A'}
              </p>
            </div>
          </div>

          <div className="grid grid-cols-1 gap-6 xl:grid-cols-2">
            <article className="chart-frame p-4">
              <div className="mb-3 flex items-center justify-between gap-2">
                <h3 className="text-sm font-semibold text-slate-800">Overall vs VAEP trend</h3>
                <div className="inline-flex items-center gap-1">
                  <button
                    type="button"
                    className="btn-secondary px-2.5 py-1 text-xs"
                    onClick={() => exportChart(overallChartRef, 'png', `${playerName}-overall-vaep-trend`)}
                  >
                    <Download className="mr-1 h-3.5 w-3.5" />PNG
                  </button>
                  <button
                    type="button"
                    className="btn-secondary px-2.5 py-1 text-xs"
                    onClick={() => exportChart(overallChartRef, 'svg', `${playerName}-overall-vaep-trend`)}
                  >
                    SVG
                  </button>
                </div>
              </div>
              <div className="h-[280px] w-full" ref={overallChartRef}>
                <ResponsiveContainer width="100%" height="100%">
                  <LineChart data={historySeriesFiltered} margin={{ top: 8, right: 12, left: 0, bottom: 8 }}>
                    <CartesianGrid strokeDasharray="3 3" stroke="#dbeafe" />
                    <XAxis dataKey="label" tick={{ fill: '#475569', fontSize: 12 }} />
                    <YAxis tick={{ fill: '#475569', fontSize: 12 }} />
                    <Tooltip
                      contentStyle={{ borderRadius: 12, borderColor: '#bfdbfe', background: '#ffffff' }}
                      labelStyle={{ color: '#1e293b' }}
                    />
                    <Legend />
                    <Line
                      type="monotone"
                      dataKey="overall"
                      name="Overall"
                      stroke="#2563eb"
                      strokeWidth={2.5}
                      dot={{ r: 2 }}
                      activeDot={{ r: 5 }}
                      isAnimationActive
                      animationDuration={1000}
                    />
                    <Line
                      type="monotone"
                      dataKey="vaep"
                      name="VAEP"
                      stroke="#059669"
                      strokeWidth={2.5}
                      dot={{ r: 2 }}
                      activeDot={{ r: 5 }}
                      isAnimationActive
                      animationDuration={1250}
                    />
                  </LineChart>
                </ResponsiveContainer>
              </div>
            </article>

            <article className="chart-frame p-4">
              <div className="mb-3 flex items-center justify-between gap-2">
                <h3 className="text-sm font-semibold text-slate-800">VAEP area flow</h3>
                <div className="inline-flex items-center gap-1">
                  <button
                    type="button"
                    className="btn-secondary px-2.5 py-1 text-xs"
                    onClick={() => exportChart(vaepChartRef, 'png', `${playerName}-vaep-area-flow`)}
                  >
                    <Download className="mr-1 h-3.5 w-3.5" />PNG
                  </button>
                  <button
                    type="button"
                    className="btn-secondary px-2.5 py-1 text-xs"
                    onClick={() => exportChart(vaepChartRef, 'svg', `${playerName}-vaep-area-flow`)}
                  >
                    SVG
                  </button>
                </div>
              </div>
              <div className="h-[280px] w-full" ref={vaepChartRef}>
                <ResponsiveContainer width="100%" height="100%">
                  <AreaChart data={historySeriesFiltered} margin={{ top: 8, right: 12, left: 0, bottom: 8 }}>
                    <defs>
                      <linearGradient id="vaepGradientAnimated" x1="0" y1="0" x2="0" y2="1">
                        <stop offset="0%" stopColor="#22c55e" stopOpacity={0.55} />
                        <stop offset="100%" stopColor="#22c55e" stopOpacity={0.08} />
                      </linearGradient>
                    </defs>
                    <CartesianGrid strokeDasharray="3 3" stroke="#dcfce7" />
                    <XAxis dataKey="label" tick={{ fill: '#475569', fontSize: 12 }} />
                    <YAxis tick={{ fill: '#475569', fontSize: 12 }} />
                    <Tooltip
                      contentStyle={{ borderRadius: 12, borderColor: '#86efac', background: '#ffffff' }}
                      labelStyle={{ color: '#1e293b' }}
                    />
                    <Area
                      type="monotone"
                      dataKey="vaep"
                      stroke="#16a34a"
                      fill="url(#vaepGradientAnimated)"
                      strokeWidth={2.5}
                      isAnimationActive
                      animationDuration={1200}
                    />
                  </AreaChart>
                </ResponsiveContainer>
              </div>
            </article>
          </div>

          <div className="mt-6 grid grid-cols-1 gap-6 lg:grid-cols-2">
            <article className="chart-frame p-4">
              <h3 className="mb-3 text-sm font-semibold text-slate-800">Performance Radar</h3>
              <div className="h-[310px] w-full">
                <ResponsiveContainer width="100%" height="100%">
                  <BarChart data={radarChartData} layout="vertical" margin={{ top: 5, right: 20, left: 90, bottom: 5 }}>
                    <CartesianGrid strokeDasharray="3 3" stroke="#e2e8f0" />
                    <XAxis type="number" tick={{ fill: '#475569', fontSize: 11 }} domain={[0, 10]} />
                    <YAxis dataKey="name" type="category" tick={{ fill: '#475569', fontSize: 11 }} width={85} />
                    <Tooltip contentStyle={{ borderRadius: 8, background: '#ffffff', border: '1px solid #dbeafe' }} />
                    <Bar dataKey="value" fill="#6366f1" minPointSize={3} isAnimationActive animationDuration={1200} />
                  </BarChart>
                </ResponsiveContainer>
              </div>
            </article>

            <article className="chart-frame p-4">
              <h3 className="mb-3 text-sm font-semibold text-slate-800">Performance Trend</h3>
              <div className="h-[310px] w-full">
                <ResponsiveContainer width="100%" height="100%">
                  <AreaChart data={trendData} margin={{ top: 8, right: 12, left: 0, bottom: 8 }}>
                    <defs>
                      <linearGradient id="trendGradientAnimated" x1="0" y1="0" x2="0" y2="1">
                        <stop offset="0%" stopColor="#0ea5e9" stopOpacity={0.5} />
                        <stop offset="100%" stopColor="#0ea5e9" stopOpacity={0.05} />
                      </linearGradient>
                    </defs>
                    <CartesianGrid strokeDasharray="3 3" stroke="#cffafe" />
                    <XAxis dataKey="date" tick={{ fill: '#475569', fontSize: 10 }} />
                    <YAxis tick={{ fill: '#475569', fontSize: 11 }} domain={[0, 10]} />
                    <Tooltip contentStyle={{ borderRadius: 8, background: '#ffffff', border: '1px solid #cffafe' }} />
                    <Area
                      type="monotone"
                      dataKey="overall"
                      stroke="#0891b2"
                      fill="url(#trendGradientAnimated)"
                      strokeWidth={2}
                      isAnimationActive
                      animationDuration={1200}
                    />
                  </AreaChart>
                </ResponsiveContainer>
              </div>
            </article>

            <article className="chart-frame p-4">
              <h3 className="mb-3 text-sm font-semibold text-slate-800">Score Breakdown</h3>
              <div className="h-[310px] w-full">
                <ResponsiveContainer width="100%" height="100%">
                  <BarChart data={breakdownData} layout="vertical" margin={{ top: 5, right: 20, left: 90, bottom: 5 }}>
                    <CartesianGrid strokeDasharray="3 3" stroke="#e2e8f0" />
                    <XAxis type="number" tick={{ fill: '#475569', fontSize: 11 }} domain={[0, 10]} />
                    <YAxis dataKey="name" type="category" tick={{ fill: '#475569', fontSize: 11 }} width={85} />
                    <Tooltip contentStyle={{ borderRadius: 8, background: '#ffffff', border: '1px solid #dbeafe' }} />
                    <Bar dataKey="value" fill="#f97316" radius={[0, 4, 4, 0]} minPointSize={3} isAnimationActive animationDuration={1200} />
                  </BarChart>
                </ResponsiveContainer>
              </div>
            </article>

            <article className="chart-frame p-4">
              <h3 className="mb-3 text-sm font-semibold text-slate-800">VAEP Analysis</h3>
              <div className="grid grid-cols-1 gap-4 md:grid-cols-2">
                <div className="h-[260px]">
                  <ResponsiveContainer width="100%" height="100%">
                    <BarChart data={vaepData.timeline} margin={{ top: 8, right: 8, left: 0, bottom: 8 }}>
                      <CartesianGrid strokeDasharray="3 3" stroke="#e2e8f0" />
                      <XAxis dataKey="date" tick={{ fill: '#475569', fontSize: 10 }} />
                      <YAxis tick={{ fill: '#475569', fontSize: 11 }} />
                      <Tooltip contentStyle={{ borderRadius: 8, background: '#ffffff', border: '1px solid #dbeafe' }} />
                      <Bar dataKey="vaep" fill="#06b6d4" minPointSize={3} isAnimationActive animationDuration={1200} />
                    </BarChart>
                  </ResponsiveContainer>
                </div>
                <div className="flex flex-col items-center justify-center rounded-lg border border-slate-200 bg-white/50 p-4">
                  <h4 className="mb-4 text-xs font-semibold uppercase text-slate-600">Season Totals</h4>
                  <div className="grid w-full grid-cols-2 gap-3 text-center">
                    <div className="rounded-lg bg-cyan-50 p-3">
                      <p className="text-xs text-slate-600">Offensive</p>
                      <p className="mt-1 text-lg font-bold text-cyan-700">{Number(vaepData.totals.offensive || 0).toFixed(2)}</p>
                    </div>
                    <div className="rounded-lg bg-emerald-50 p-3">
                      <p className="text-xs text-slate-600">Defensive</p>
                      <p className="mt-1 text-lg font-bold text-emerald-700">{Number(vaepData.totals.defensive || 0).toFixed(2)}</p>
                    </div>
                  </div>
                </div>
              </div>
            </article>

            <article className="chart-frame p-4">
              <h3 className="mb-3 text-sm font-semibold text-slate-800">Position Comparison</h3>
              <div className="h-[310px] w-full">
                <ResponsiveContainer width="100%" height="100%">
                  <BarChart data={positionData} layout="vertical" margin={{ top: 5, right: 20, left: 90, bottom: 5 }}>
                    <CartesianGrid strokeDasharray="3 3" stroke="#e2e8f0" />
                    <XAxis type="number" tick={{ fill: '#475569', fontSize: 11 }} domain={[0, 10]} />
                    <YAxis dataKey="name" type="category" tick={{ fill: '#475569', fontSize: 11 }} width={85} />
                    <Tooltip contentStyle={{ borderRadius: 8, background: '#ffffff', border: '1px solid #dbeafe' }} />
                    <Bar dataKey="player" name="Player" fill="#a78bfa" minPointSize={3} isAnimationActive animationDuration={1100} />
                    <Bar dataKey="position_avg" name="Position Avg" fill="#94a3b8" minPointSize={3} isAnimationActive animationDuration={1150} />
                  </BarChart>
                </ResponsiveContainer>
              </div>
            </article>

            <article className="chart-frame p-4">
              <h3 className="mb-3 text-sm font-semibold text-slate-800">Percentile Rankings</h3>
              <div className="h-[310px] w-full">
                <ResponsiveContainer width="100%" height="100%">
                  <BarChart data={percentileChartData} layout="vertical" margin={{ top: 5, right: 20, left: 110, bottom: 5 }}>
                    <CartesianGrid strokeDasharray="3 3" stroke="#e2e8f0" />
                    <XAxis type="number" tick={{ fill: '#475569', fontSize: 11 }} domain={[0, 100]} />
                    <YAxis dataKey="name" type="category" tick={{ fill: '#475569', fontSize: 11 }} width={100} />
                    <Tooltip
                      contentStyle={{ borderRadius: 8, background: '#ffffff', border: '1px solid #dbeafe' }}
                      formatter={(value) => `${value}%`}
                    />
                    <Bar dataKey="value" fill="#14b8a6" radius={[0, 4, 4, 0]} minPointSize={3} isAnimationActive animationDuration={1200} />
                  </BarChart>
                </ResponsiveContainer>
              </div>
            </article>
          </div>
        </section>
      </div>
    </div>
  );
};

export default PlayerAnimatedAnalysis;
