using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.FitnessStatus.SessionMovements.Queries.PlayerLoadVsCapacityTimeline
{
    [Authorize(Roles = "Player,FitnessCoach,HeadCoach")]
    public record GetPlayerLoadVsCapacityTimelineQuery : IRequest<ResultOf<IEnumerable<PlayerLoadVsCapacityTimelineReadModel>>>, IPlayerAuthraizedRequest
    {
        public Guid PlayerId { get; private set; }
        public DateTime? FromDate { get; init; } = null;
        public DateTime? ToDate { get; init; } = null;

        public GetPlayerLoadVsCapacityTimelineQuery(Guid playerId, DateTime? fromDate, DateTime? toDate)
        {
            PlayerId = playerId;
            FromDate = fromDate;
            ToDate = toDate;
        }

        Guid? IPlayerAuthraizedRequest.PlayerId { get { return PlayerId; } set { PlayerId = value.Value; } }
    }

    public enum MetricSourceType
    {
        CapacityTest = 1,
        SessionMovement = 2
    }

    public class PlayerLoadVsCapacityTimelineReadModel
    {
        public DateTime RecordDate { get; set; }
        public MetricSourceType SourceType { get; set; }
        public decimal Value { get; set; }
    }

}
