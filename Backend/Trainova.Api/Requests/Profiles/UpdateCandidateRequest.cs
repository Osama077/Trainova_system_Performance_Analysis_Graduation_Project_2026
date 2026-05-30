using System;
using Trainova.Application.Scouting.Candidates.Commands.UpdateCandidate;

namespace Trainova.Api.Requests.Profiles
{
    public class UpdateCandidateRequest
    {
        public string? FullName { get; set; }
        public int? Age { get; set; }
        public int? CurrentMainPosition { get; set; }
        public int? OtherAvailablePositions { get; set; }
        public decimal? PerformanceLevel { get; set; }
        public string? Note { get; set; }

        public UpdateCandidateCommand ToCommand(Guid id)
        {
            return new UpdateCandidateCommand(id, FullName, Age, CurrentMainPosition, OtherAvailablePositions, PerformanceLevel, Note);
        }
    }
}
