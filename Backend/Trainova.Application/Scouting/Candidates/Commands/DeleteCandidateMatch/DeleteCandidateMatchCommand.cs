using MediatR;
using System;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Commands.DeleteCandidateMatch
{
    [Authorize(Roles = "TeamStaff,HeadCoach,AssistantCoach,FitnessCoach")]
    public record DeleteCandidateMatchCommand(Guid CandidateId, Guid MatchId) : IRequest<ResultOf<bool>>;
}
