using MediatR;
using System;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Commands.DeleteSeasonStatistics
{
    [Authorize(Roles = "TeamStaff,HeadCoach,AssistantCoach,FitnessCoach")]
    public record DeleteSeasonStatisticsCommand(Guid CandidateId, Guid SeasonId) : IRequest<ResultOf<bool>>;
}
