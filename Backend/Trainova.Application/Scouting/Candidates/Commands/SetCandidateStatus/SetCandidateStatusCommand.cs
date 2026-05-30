using MediatR;
using Trainova.Domain.Common.Enums;
using System;

namespace Trainova.Application.Scouting.Candidates.Commands.SetCandidateStatus
{
    public record SetCandidateStatusCommand(Guid CandidateId, CandidateStatus Flags, bool Add, string? Note) : IRequest<bool>;
}
