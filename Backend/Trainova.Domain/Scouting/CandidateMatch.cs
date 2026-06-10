using System;
using Trainova.Domain.Common.Helpers;

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
        public float Rating { get; private set; } // Scout's rating for this match (0.0 – 10.0)
        public string? ScoutNotes { get; private set; } // Scout notes specific to this match performance

        private CandidateMatch() { }

        public CandidateMatch(Guid candidateId, DateTime matchDate, string matchName, int goals, int assists, float rating, string? scoutNotes)
        {
            if (rating < 0 || rating > 10.0f)
                throw new DomainException("Match rating must be between 0.0 and 10.0.", "DomainError_InvalidRatingValue");

            Id = Guid.NewGuid();
            CandidateId = candidateId;
            MatchDate = matchDate;
            MatchName = matchName ?? string.Empty;
            Goals = goals;
            Assists = assists;
            Rating = rating;
            ScoutNotes = scoutNotes;
        }
    }
}
