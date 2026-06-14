using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.FitnessStatus.PhysicalCapacityTests.Queries.GetPhysicalCapacityTests
{
    public record GetPhysicalCapacityTestsQuery : IRequest<ResultOf<IEnumerable<PlayerPhysicalCapacityTestReadModel>>>, IPlayerAuthraizedRequest
    {
        public Guid? PlayerId { get; private set; } = null;
        public string? SearchName { get; init; } = null;
        public DateTime? FromDate { get; init; } = null;
        public DateTime? ToDate { get; init; } = null;

        public GetPhysicalCapacityTestsQuery(Guid? playerId, string? searchName, DateTime? fromDate, DateTime? toDate)
        {
            PlayerId = playerId;
            SearchName = searchName;
            FromDate = fromDate;
            ToDate = toDate;
        }


        Guid? IPlayerAuthraizedRequest.PlayerId { get { return PlayerId; } set { PlayerId = value; } }
    }



}
