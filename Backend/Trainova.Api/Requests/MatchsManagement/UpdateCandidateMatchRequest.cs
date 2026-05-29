using Trainova.Application.MatchsManagement.Matches.Commands.UpdateCandidateMatch;

namespace Trainova.Api.Requests.MatchsManagement
{
    public class UpdateCandidateMatchRequest
    {
        public Guid? CandidateId { get; set; }
        public DateTime? MatchDate { get; set; }
        public string? OpponentName { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public string? Notes { get; set; }

        public UpdateCandidateMatchCommand ToCommand(Guid id)
        {
            return new UpdateCandidateMatchCommand(id, CandidateId, MatchDate, OpponentName, HomeScore, AwayScore, Notes);
        }
    }
}
