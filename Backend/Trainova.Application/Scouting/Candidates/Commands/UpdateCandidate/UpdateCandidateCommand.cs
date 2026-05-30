using MediatR;
using System;

namespace Trainova.Application.Scouting.Candidates.Commands.UpdateCandidate
{
    public record UpdateCandidateCommand(
        Guid Id,
        string? FullName,
        int? Age,
        int? CurrentMainPosition,
        int? OtherAvailablePositions,
        decimal? PerformanceLevel,
        string? Note) : IRequest<string?>;
}
