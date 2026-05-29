using Trainova.Application.MatchsManagement.Matches.Commands.CreateCandidateMatch;

namespace Trainova.Api.Requests.MatchsManagement
{
    public class CreateCandidateMatchRequest
    {
        public Guid CandidateId { get; set; }
        public DateTime MatchDate { get; set; }
        public string OpponentName { get; set; } = null!;
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public string? Notes { get; set; }

        public CreateCandidateMatchCommand ToCommand()
        {
            return new CreateCandidateMatchCommand(CandidateId, MatchDate, OpponentName, HomeScore, AwayScore, Notes);
        }
    }
}
