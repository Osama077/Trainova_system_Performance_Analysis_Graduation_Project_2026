using MediatR;
using Trainova.Domain.Common.Enums;
using System;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Commands.SetCandidateStatus
{
    public record SetCandidateStatusCommand(Guid CandidateId, CandidateStatus Flags, bool Add, string? Note) : IRequest<ResultOf<bool>>;
}
