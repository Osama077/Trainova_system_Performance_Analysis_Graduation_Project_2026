/**
 * API Client for Match Performance Analysis Backend
 * Communicates with FastAPI server at http://localhost:8000/api/v1
 */

import axios from 'axios';

const API_BASE_URL = process.env.REACT_APP_API_URL || 'http://localhost:8000/api/v1';

// Create axios instance with default config
const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  timeout: 30000,
});

// Error interceptor for consistent error handling
apiClient.interceptors.response.use(
  (response) => response,
  (error) => {
    console.error('API Error:', error.response?.data || error.message);
    return Promise.reject(error);
  }
);

/**
 * PLAYER ENDPOINTS
 */
export const PlayerAPI = {
  // Get list of all players
  getPlayerList: async () => {
    try {
      const response = await apiClient.get('/player/list');
      return response.data;
    } catch (error) {
      throw new Error(`Failed to fetch player list: ${error.message}`);
    }
  },

  // Get player dashboard with 9 charts
  getPlayerDashboard: async (playerName, matchId = null) => {
    try {
      const query = matchId ? `?match_id=${encodeURIComponent(matchId)}` : '';
      const response = await apiClient.get(
        `/player/dashboard/${encodeURIComponent(playerName)}${query}`,
        { timeout: 180000 }
      );
      return response.data;
    } catch (error) {
      throw new Error(`Failed to fetch player dashboard for ${playerName}: ${error.message}`);
    }
  },

  // Get player dashboard raw data for frontend rendering with animation
  getPlayerDashboardData: async (playerName, matchId = null) => {
    try {
      const query = matchId ? `?match_id=${encodeURIComponent(matchId)}` : '';
      const response = await apiClient.get(
        `/player/dashboard-data/${encodeURIComponent(playerName)}${query}`,
        { timeout: 180000 }
      );
      return response.data;
    } catch (error) {
      throw new Error(`Failed to fetch player dashboard data for ${playerName}: ${error.message}`);
    }
  },

  // Get player score
  getPlayerScore: async (playerId) => {
    try {
      const response = await apiClient.get(`/player/${playerId}/score`);
      return response.data;
    } catch (error) {
      throw new Error(`Failed to fetch player score: ${error.message}`);
    }
  },

  // Get player stats
  getPlayerStats: async (playerId) => {
    try {
      const response = await apiClient.get(`/player/${playerId}/stats`);
      return response.data;
    } catch (error) {
      throw new Error(`Failed to fetch player stats: ${error.message}`);
    }
  },

  // Get player history
  getPlayerHistory: async (playerId) => {
    try {
      const response = await apiClient.get(`/player/${playerId}/history`);
      return response.data;
    } catch (error) {
      throw new Error(`Failed to fetch player history: ${error.message}`);
    }
  },

  // Compare multiple players
  comparePlayer: async (playerIds) => {
    try {
      // Format: "1,2,3"
      const queryString = playerIds.join(',');
      const response = await apiClient.get(`/player/compare?player_ids=${queryString}`);
      return response.data;
    } catch (error) {
      throw new Error(`Failed to compare players: ${error.message}`);
    }
  },
};

/**
 * TEAM ENDPOINTS
 */
export const TeamAPI = {
  // Get team summary
  getTeamSummary: async (teamId) => {
    try {
      const response = await apiClient.get(`/team/${encodeURIComponent(teamId)}/summary`);
      return response.data;
    } catch (error) {
      throw new Error(`Failed to fetch team summary: ${error.message}`);
    }
  },

  // Get team heatmap data
  getTeamHeatmap: async (teamId, matchId, playerId = null) => {
    try {
      if (!matchId) {
        throw new Error('matchId is required for team heatmap');
      }
      const params = new URLSearchParams({ match_id: String(matchId) });
      if (playerId !== null && playerId !== undefined) {
        params.append('player_id', String(playerId));
      }
      const response = await apiClient.get(
        `/team/${encodeURIComponent(teamId)}/heatmap?${params.toString()}`
      );
      return response.data;
    } catch (error) {
      throw new Error(`Failed to fetch team heatmap: ${error.message}`);
    }
  },
};

/**
 * MATCH ENDPOINTS
 */
export const MatchAPI = {
  // Get match report
  getMatchReport: async (matchId) => {
    try {
      const response = await apiClient.get(`/match/${matchId}/report`);
      return response.data;
    } catch (error) {
      throw new Error(`Failed to fetch match report: ${error.message}`);
    }
  },

  // Get match events
  getMatchEvents: async (matchId) => {
    try {
      const response = await apiClient.get(`/match/${matchId}/events`);
      return response.data;
    } catch (error) {
      throw new Error(`Failed to fetch match events: ${error.message}`);
    }
  },
};

/**
 * ANALYSIS ENDPOINTS
 */
export const AnalysisAPI = {
  // Analyze specific match
  analyzeMatch: async (matchId) => {
    try {
      const response = await apiClient.post(`/analyze/match/${matchId}`);
      return response.data;
    } catch (error) {
      throw new Error(`Failed to analyze match: ${error.message}`);
    }
  },

  // Analyze season
  analyzeSeason: async () => {
    try {
      const response = await apiClient.post('/analyze/season');
      return response.data;
    } catch (error) {
      throw new Error(`Failed to analyze season: ${error.message}`);
    }
  },
};

/**
 * BENCHMARK ENDPOINTS
 */
export const BenchmarkAPI = {
  // Get benchmark for position
  getBenchmark: async (position) => {
    try {
      const response = await apiClient.get(`/benchmark/${position}`);
      return response.data;
    } catch (error) {
      throw new Error(`Failed to fetch benchmark for ${position}: ${error.message}`);
    }
  },
};

/**
 * Health Check
 */
export const HealthAPI = {
  checkHealth: async () => {
    try {
      const response = await apiClient.get('/');
      return response.data;
    } catch (error) {
      console.error('API health check failed:', error.message);
      return null;
    }
  },
};

export default apiClient;
