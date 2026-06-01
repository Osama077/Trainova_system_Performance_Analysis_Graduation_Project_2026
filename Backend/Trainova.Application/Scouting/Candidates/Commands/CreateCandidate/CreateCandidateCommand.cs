using MediatR;
using System;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Commands.CreateCandidate
{
    public record CreateCandidateCommand(
        string FullName,
        int Age,
        int Position,
        float PerformanceScore,
        float InjuryRisk,
        int CurrentMainPosition,
        int OtherAvailablePositions,
        decimal PerformanceLevel,
        Guid? CurrentTeamId) : IRequest<ResultOf<Guid>>;
}
