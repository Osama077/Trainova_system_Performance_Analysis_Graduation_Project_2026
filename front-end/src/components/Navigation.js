import React, { useState } from 'react';
import {
  Activity,
  BarChart3,
  CheckCircle2,
  Gauge,
  Home,
  List,
  RefreshCw,
  SlidersHorizontal,
  Users,
  WifiOff,
} from 'lucide-react';
import { HealthAPI } from '../api';
import { PAGES, useAppContext, USER_ROLES } from '../context/AppContext';

const navItems = [
  { id: PAGES.OVERVIEW, label: 'Overview', icon: Home },
  { id: PAGES.PLAYERS, label: 'Players', icon: List },
  { id: PAGES.DASHBOARD, label: 'Dashboard', icon: BarChart3 },
  { id: PAGES.ANIMATED, label: 'Animated', icon: Activity },
  { id: PAGES.COMPARISON, label: 'Comparison', icon: Users },
  { id: PAGES.API_TESTER, label: 'API Tests', icon: SlidersHorizontal },
];

const roleOptions = [
  { value: USER_ROLES.ANALYST, label: 'Analyst' },
  { value: USER_ROLES.COACH, label: 'Coach' },
  { value: USER_ROLES.ADMIN, label: 'Admin' },
];

const Navigation = () => {
  const { currentPage, navigate, userRole, setUserRole } = useAppContext();
  const [serverStatus, setServerStatus] = useState('checking');
  const [lastCheck, setLastCheck] = useState(null);

  React.useEffect(() => {
    const checkHealth = async () => {
      try {
        const result = await HealthAPI.checkHealth();
        setServerStatus(result ? 'online' : 'offline');
        setLastCheck(new Date());
      } catch (err) {
        setServerStatus('offline');
        setLastCheck(new Date());
      }
    };

    checkHealth();
    const interval = setInterval(checkHealth, 30000);
    return () => clearInterval(interval);
  }, []);

  const statusIcon =
    serverStatus === 'online' ? <CheckCircle2 className="h-4 w-4 text-emerald-500" /> :
    serverStatus === 'checking' ? <RefreshCw className="h-4 w-4 animate-spin text-amber-500" /> :
    <WifiOff className="h-4 w-4 text-rose-500" />;

  const statusText =
    serverStatus === 'online' ? 'API online' :
    serverStatus === 'checking' ? 'Checking API' :
    'API offline';

  return (
    <nav className="sticky top-0 z-30 border-b border-white/60 bg-white/75 backdrop-blur">
      <div className="mx-auto w-full max-w-7xl px-4 py-3 sm:px-6 lg:px-8">
        <div className="flex flex-col gap-3 lg:flex-row lg:items-center lg:justify-between">
          <div className="flex items-center gap-3">
            <div className="rounded-xl bg-gradient-to-br from-brand-600 via-cyan-600 to-violet-600 p-2 text-white shadow-glow">
              <Gauge className="h-5 w-5" />
            </div>
            <div>
              <h1 className="text-base font-semibold text-slate-900 sm:text-lg">Match Performance Workbench</h1>
              <p className="text-xs text-slate-500">AI-ready analytics interface for football operations</p>
            </div>
          </div>

          <div className="flex flex-wrap items-center gap-3">
            <label className="text-xs font-medium uppercase tracking-wide text-slate-500" htmlFor="role-select">
              Role
            </label>
            <select
              id="role-select"
              value={userRole}
              onChange={(e) => setUserRole(e.target.value)}
              className="field w-auto min-w-32"
              aria-label="Select user role"
            >
              {roleOptions.map((opt) => (
                <option key={opt.value} value={opt.value}>{opt.label}</option>
              ))}
            </select>

            <div className="inline-flex items-center gap-2 rounded-xl border border-white/70 bg-white/75 px-3 py-2 text-xs text-slate-600">
              {statusIcon}
              <span className="font-medium">{statusText}</span>
              {lastCheck ? <span className="text-slate-400">{lastCheck.toLocaleTimeString()}</span> : null}
            </div>
          </div>
        </div>

        <div className="mt-3 flex flex-wrap gap-2" role="tablist" aria-label="Application navigation">
          {navItems.map((item) => (
            <button
              key={item.id}
              onClick={() => navigate(item.id)}
              className={`inline-flex items-center gap-2 rounded-xl px-4 py-2 text-sm font-medium transition ${
                currentPage === item.id
                  ? 'bg-gradient-to-r from-brand-600 via-cyan-600 to-violet-600 text-white shadow-glow'
                  : 'bg-white/75 text-slate-700 hover:bg-white'
              }`}
              role="tab"
              aria-selected={currentPage === item.id}
            >
              <item.icon className="h-4 w-4" />
              {item.label}
            </button>
          ))}
        </div>
      </div>
    </nav>
  );
};

export default Navigation;
