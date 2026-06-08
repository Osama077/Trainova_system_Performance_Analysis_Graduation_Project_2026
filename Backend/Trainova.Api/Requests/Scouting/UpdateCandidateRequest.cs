using System;
using Trainova.Application.Scouting.Candidates.Commands.UpdateCandidate;

namespace Trainova.Api.Requests.Scouting
{
    public class UpdateCandidateRequest
    {
        public string? FullName { get; set; }
        public int? Age { get; set; }
        public string? CurrentTeamName { get; set; }

        public UpdateCandidateCommand ToCommand(Guid id)
        {
            return new UpdateCandidateCommand(id, FullName, Age, CurrentTeamName);
        }
    }
}
