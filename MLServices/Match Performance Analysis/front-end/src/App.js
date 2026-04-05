import React from 'react';
import { Activity, ShieldCheck } from 'lucide-react';
import { AppProvider, PAGES, useAppContext, USER_ROLES } from './context/AppContext';
import Navigation from './components/Navigation';
import HomePage from './components/HomePage';
import PlayerList from './components/PlayerList';
import PlayerDashboard from './components/PlayerDashboard';
import PlayerAnimatedAnalysis from './components/PlayerAnimatedAnalysis';
import PlayerComparison from './components/PlayerComparison';
import APITester from './components/APITester';

function AppLayout() {
  const { currentPage, selectedPlayerName, userRole, navigate, openPlayerDashboard } = useAppContext();

  const showApiTester = userRole === USER_ROLES.ADMIN || userRole === USER_ROLES.ANALYST;

  return (
    <div className="page-shell">
      <Navigation />

      <main className="mx-auto w-full max-w-7xl px-4 pb-10 pt-6 sm:px-6 lg:px-8 animate-in">
        {currentPage === PAGES.OVERVIEW && <HomePage onNavigate={navigate} />}

        {currentPage === PAGES.PLAYERS && <PlayerList onSelectPlayer={openPlayerDashboard} />}

        {currentPage === PAGES.DASHBOARD && (
          selectedPlayerName ? (
            <PlayerDashboard playerName={selectedPlayerName} />
          ) : (
            <div className="surface p-8 text-center">
              <p className="text-sm text-slate-600">Select a player first from the Players section.</p>
            </div>
          )
        )}

        {currentPage === PAGES.ANIMATED && (
          selectedPlayerName ? (
            <PlayerAnimatedAnalysis playerName={selectedPlayerName} />
          ) : (
            <div className="surface p-8 text-center">
              <p className="text-sm text-slate-600">Select a player first from the Players section.</p>
            </div>
          )
        )}

        {currentPage === PAGES.COMPARISON && <PlayerComparison />}

        {currentPage === PAGES.API_TESTER && showApiTester && <APITester />}
      </main>

      <footer className="border-t border-white/60 bg-white/70 backdrop-blur">
        <div className="mx-auto flex w-full max-w-7xl flex-col items-center justify-between gap-3 px-4 py-5 text-xs text-slate-500 sm:flex-row sm:px-6 lg:px-8">
          <div className="flex items-center gap-2">
            <Activity className="h-4 w-4" />
            <span>Match Performance Analysis Platform</span>
          </div>
          <p className="flex items-center gap-1">
            <ShieldCheck className="h-4 w-4" />
            Production-ready UI with resilient API flows
          </p>
        </div>
      </footer>
    </div>
  );
}

function App() {
  return (
    <AppProvider>
      <AppLayout />
    </AppProvider>
  );
}

export default App;
