using System;
using Trainova.Application.Scouting.Candidates.Commands.CreateCandidate;

namespace Trainova.Api.Requests.Scouting
{
    public class CreateCandidateRequest
    {
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public int Position { get; set; }
        public float PerformanceScore { get; set; }
        public float InjuryRisk { get; set; }
        public int CurrentMainPosition { get; set; }
        public int OtherAvailablePositions { get; set; }
        public decimal PerformanceLevel { get; set; }
        public Guid? CurrentTeamId { get; set; }

        public CreateCandidateCommand ToCommand()
        {
            return new CreateCandidateCommand(FullName, Age, Position, PerformanceScore, InjuryRisk, CurrentMainPosition, OtherAvailablePositions, PerformanceLevel, CurrentTeamId);
        }
    }
}
