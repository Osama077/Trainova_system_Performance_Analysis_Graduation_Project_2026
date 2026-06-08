using System;

namespace Trainova.Domain.Scouting
{
    public class CandidateMatch
    {
        public Guid Id { get; private set; }
        public Guid CandidateId { get; private set; }
        public DateTime MatchDate { get; private set; }
        public string MatchName { get; private set; } = string.Empty; // e.g., "Al Ahly vs Zamalek"
        public int Goals { get; private set; }
        public int Assists { get; private set; }
        public float Rating { get; private set; } // Scout's rating for this match
        public string? ScoutNotes { get; private set; } // Scout notes specific to this match performance
        public Guid? CreatedBy { get; private set; } // Scout who logged this match
        public DateTime CreatedAt { get; private set; }

        private CandidateMatch() { }

        public CandidateMatch(Guid candidateId, DateTime matchDate, string matchName, int goals, int assists, float rating, string? scoutNotes, Guid? createdBy)
        {
            Id = Guid.NewGuid();
            CandidateId = candidateId;
            MatchDate = matchDate;
            MatchName = matchName ?? string.Empty;
            Goals = goals;
            Assists = assists;
            Rating = rating;
            ScoutNotes = scoutNotes;
            CreatedBy = createdBy;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
