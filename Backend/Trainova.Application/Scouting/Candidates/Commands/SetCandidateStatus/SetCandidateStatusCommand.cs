using MediatR;
using Trainova.Domain.Common.Enums;
using System;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Commands.SetCandidateStatus
{
    // Removed Replace flag - command carries status add/remove
    [Authorize(Roles = "TeamStaff,HeadCoach,AssistantCoach,FitnessCoach")]
    public record SetCandidateStatusCommand(Guid CandidateId, CandidateStatus Status, bool Add) : IRequest<ResultOf<bool>>;
}
