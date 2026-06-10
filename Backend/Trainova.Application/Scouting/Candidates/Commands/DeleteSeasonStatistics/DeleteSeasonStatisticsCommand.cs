using MediatR;
using System;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Commands.DeleteSeasonStatistics
{
    public record DeleteSeasonStatisticsCommand(Guid CandidateId, Guid SeasonId) : IRequest<ResultOf<bool>>;
}
