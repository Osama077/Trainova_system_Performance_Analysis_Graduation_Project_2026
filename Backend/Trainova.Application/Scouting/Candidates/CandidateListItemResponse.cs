using System;

namespace Trainova.Application.Scouting.Candidates
{
    public class CandidateListItemResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Avatar { get; set; }
        public int Age { get; set; }
        public int Position { get; set; }
        public Guid? CurrentTeamId { get; set; }
        public string? CurrentTeamName { get; set; }
        public string? Nationality { get; set; }
        public string? CountryFlag { get; set; }
        public float PerformanceScore { get; set; }
        public float ScoutRating { get; set; }
        public decimal PerformanceLevel { get; set; }
        public int Status { get; set; }

        // Season stats shown on cards / list
        public int? Goals { get; set; }
        public int? Assists { get; set; }
        public int? Matches { get; set; }

        // Shortlist / ranking
        public int? ShortlistRank { get; set; }
        public bool? IsOnTrial { get; set; }

        // Skills summary (mini skill bars)
        public int? Pace { get; set; }
        public int? Shooting { get; set; }
        public int? Dribbling { get; set; }
        public int? Passing { get; set; }
        public int? Physicality { get; set; }
        public int? Positioning { get; set; }
        public int? Defending { get; set; }
        public int? Vision { get; set; }

        // Contract / market
        public DateTime? ContractEnd { get; set; }
        public decimal? MarketValue { get; set; }
        public string? Agent { get; set; }

        // Misc
        public int? MatchesWatchedCount { get; set; }
        public string? NotesSnippet { get; set; }
    }
}
