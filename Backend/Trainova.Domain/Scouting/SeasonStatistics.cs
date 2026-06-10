using System;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Domain.Scouting
{
    public class SeasonStatistics
    {
        public Guid Id { get; private set; }
        public Guid CandidateId { get; private set; }
        public string Season { get; private set; } = string.Empty;
        public string League { get; private set; } = string.Empty;
        public int Goals { get; private set; }
        public int Assists { get; private set; }
        public int Matches { get; private set; }
        public float PassAccuracy { get; private set; }
        public float ShotsPer90 { get; private set; }
        public float XgPer90 { get; private set; }

        private SeasonStatistics() { }

        public SeasonStatistics(Guid candidateId, string season, string league, int goals, int assists, int matches, float passAccuracy, float shotsPer90, float xgPer90)
        {
            if (string.IsNullOrWhiteSpace(season))
                throw new DomainException("Season is required.", "DomainError_SeasonRequired");
            if (string.IsNullOrWhiteSpace(league))
                throw new DomainException("League is required.", "DomainError_LeagueRequired");
            if (passAccuracy < 0 || passAccuracy > 100)
                throw new DomainException("Pass accuracy must be between 0 and 100.", "DomainError_InvalidPassAccuracy");
            if (shotsPer90 < 0)
                throw new DomainException("Shots per 90 must be non-negative.", "DomainError_InvalidShotsPer90");
            if (xgPer90 < 0)
                throw new DomainException("xG per 90 must be non-negative.", "DomainError_InvalidXgPer90");

            Id = Guid.NewGuid();
            CandidateId = candidateId;
            Season = season;
            League = league;
            Goals = goals;
            Assists = assists;
            Matches = matches;
            PassAccuracy = passAccuracy;
            ShotsPer90 = shotsPer90;
            XgPer90 = xgPer90;
        }
    }
}
