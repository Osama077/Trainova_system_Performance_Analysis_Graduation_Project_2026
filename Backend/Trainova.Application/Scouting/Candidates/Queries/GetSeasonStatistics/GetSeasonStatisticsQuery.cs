using MediatR;
using System;
using System.Collections.Generic;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Queries.GetSeasonStatistics
{
    public record GetSeasonStatisticsQuery(Guid CandidateId) : IRequest<ResultOf<IEnumerable<SeasonStatisticsResponse>>>;
}
