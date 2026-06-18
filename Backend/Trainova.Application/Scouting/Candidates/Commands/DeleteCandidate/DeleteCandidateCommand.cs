using MediatR;
using System;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Commands.DeleteCandidate
{
    [Authorize(Roles = "TeamStaff,HeadCoach,AssistantCoach,FitnessCoach")]
    public record DeleteCandidateCommand(Guid CandidateId) : IRequest<ResultOf<bool>>;
}
