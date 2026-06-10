using System;

namespace Trainova.Application.Scouting.Candidates.Queries.GetCandidateMatches
{
    public class CandidateMatchResponse
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public DateTime MatchDate { get; set; }
        public string MatchName { get; set; } = string.Empty;
        public int Goals { get; set; }
        public int Assists { get; set; }
        public float Rating { get; set; }
        public string? ScoutNotes { get; set; }
    }
}
