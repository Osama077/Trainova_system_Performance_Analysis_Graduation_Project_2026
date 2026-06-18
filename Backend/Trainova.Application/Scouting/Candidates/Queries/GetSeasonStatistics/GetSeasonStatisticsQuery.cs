using MediatR;
using System;
using System.Collections.Generic;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Queries.GetSeasonStatistics
{
    [Authorize(Roles = "TeamStaff,HeadCoach,AssistantCoach,FitnessCoach")]
    public record GetSeasonStatisticsQuery(Guid CandidateId) : IRequest<ResultOf<IEnumerable<SeasonStatisticsResponse>>>;
}
