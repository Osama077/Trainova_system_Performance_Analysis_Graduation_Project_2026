using MediatR;
using System;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Commands.UpdateCandidate
{
    [System.Obsolete("UpdateCandidateCommand is deprecated: candidate edits are no longer supported via PUT. Use specific endpoints (status, notes) instead.")]
    public record UpdateCandidateCommand(
        Guid Id,
        string? FullName,
        int? Age,
        string? CurrentTeamName) : IRequest<ResultOf<string?>>;
}
