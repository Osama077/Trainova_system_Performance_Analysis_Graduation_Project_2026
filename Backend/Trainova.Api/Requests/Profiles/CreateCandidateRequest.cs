using Trainova.Application.Profiles.Candidates.Commands.CreateCandidate;

namespace Trainova.Api.Requests.Profiles
{
    public class CreateCandidateRequest
    {
        public string FullName { get; set; } = null!;
        public DateTime ScoutedAt { get; set; }
        public string? Email { get; set; }

        public CreateCandidateCommand ToCommand()
        {
            return new CreateCandidateCommand(FullName, ScoutedAt, Email);
        }
    }
}
