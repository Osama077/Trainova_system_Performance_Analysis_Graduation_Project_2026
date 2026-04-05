import React, { createContext, useContext, useMemo, useState } from 'react';

const AppContext = createContext(null);

export const PAGES = {
  OVERVIEW: 'overview',
  PLAYERS: 'players',
  DASHBOARD: 'dashboard',
  ANIMATED: 'animated',
  COMPARISON: 'comparison',
  API_TESTER: 'api_tester',
};

export const USER_ROLES = {
  ANALYST: 'analyst',
  COACH: 'coach',
  ADMIN: 'admin',
};

export function AppProvider({ children }) {
  const [currentPage, setCurrentPage] = useState(PAGES.OVERVIEW);
  const [selectedPlayerName, setSelectedPlayerName] = useState('');
  const [userRole, setUserRole] = useState(USER_ROLES.ANALYST);

  const navigate = (page) => setCurrentPage(page);

  const openPlayerDashboard = (playerName) => {
    setSelectedPlayerName(playerName);
    setCurrentPage(PAGES.DASHBOARD);
  };

  const value = useMemo(
    () => ({
      currentPage,
      selectedPlayerName,
      userRole,
      navigate,
      setUserRole,
      openPlayerDashboard,
    }),
    [currentPage, selectedPlayerName, userRole]
  );

  return <AppContext.Provider value={value}>{children}</AppContext.Provider>;
}

export function useAppContext() {
  const ctx = useContext(AppContext);
  if (!ctx) {
    throw new Error('useAppContext must be used within AppProvider');
  }
  return ctx;
}
