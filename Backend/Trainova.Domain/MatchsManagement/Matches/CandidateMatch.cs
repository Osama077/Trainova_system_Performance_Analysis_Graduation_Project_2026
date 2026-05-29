using Trainova.Domain.Common.BaseEntity;

namespace Trainova.Domain.MatchsManagement.Matches
{
    public class CandidateMatch : AuditableEntity<Guid>
    {
        public Guid CandidateId { get; private set; }
        public DateTime MatchDate { get; private set; }
        public string OpponentName { get; private set; }
        public int? HomeScore { get; private set; }
        public int? AwayScore { get; private set; }
        public string? Notes { get; private set; }

        private CandidateMatch() : base() { }

        public CandidateMatch(
            Guid id,
            Guid candidateId,
            DateTime matchDate,
            string opponentName,
            int? homeScore = null,
            int? awayScore = null,
            string? notes = null,
            Guid? createdBy = null) : base(id, createdBy)
        {
            CandidateId = candidateId;
            MatchDate = matchDate;
            OpponentName = opponentName;
            HomeScore = homeScore;
            AwayScore = awayScore;
            Notes = notes;
        }

        public void Update(DateTime? matchDate = null, string? opponentName = null, int? homeScore = null, int? awayScore = null, string? notes = null)
        {
            MarkUpdatedNow();
            MatchDate = matchDate ?? MatchDate;
            OpponentName = opponentName ?? OpponentName;
            HomeScore = homeScore ?? HomeScore;
            AwayScore = awayScore ?? AwayScore;
            Notes = notes ?? Notes;
        }
    }
}
