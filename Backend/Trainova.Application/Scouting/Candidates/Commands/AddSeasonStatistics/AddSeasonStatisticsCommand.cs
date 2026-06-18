using MediatR;
using System;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Commands.AddSeasonStatistics
{
    [Authorize(Roles = "TeamStaff,HeadCoach,AssistantCoach,FitnessCoach")]
    public record AddSeasonStatisticsCommand(
        Guid CandidateId,
        string Season,
        string League,
        int Goals,
        int Assists,
        int Matches,
        float PassAccuracy,
        float ShotsPer90,
        float XgPer90) : IRequest<ResultOf<Guid>>;
}
