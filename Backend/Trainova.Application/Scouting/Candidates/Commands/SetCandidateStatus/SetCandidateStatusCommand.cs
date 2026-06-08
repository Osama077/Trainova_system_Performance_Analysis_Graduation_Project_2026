using MediatR;
using Trainova.Domain.Common.Enums;
using System;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Commands.SetCandidateStatus
{
    // Removed Replace flag - command carries status add/remove
    public record SetCandidateStatusCommand(Guid CandidateId, CandidateStatus Status, bool Add) : IRequest<ResultOf<bool>>;
}
