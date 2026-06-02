using MediatR;
using Trainova.Domain.Common.Enums;
using System;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Commands.SetCandidateStatus
{
    // Removed Replace flag - command carries status, add/remove and optional note only
    public record SetCandidateStatusCommand(Guid CandidateId, CandidateStatus Status, bool Add, string? Note) : IRequest<ResultOf<bool>>;
}
