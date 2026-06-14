using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.FitnessStatus.PhysicalCapacityTests.Queries.GetPhysicalCapacityOverTime
{
    [Authorize(Roles = "Player,FitnessCoach,HeadCoach")]
    public record GetPhysicalCapacityOverTimeQuery : IRequest<ResultOf<IEnumerable<PhysicalCapacityDataReadModel>>>, IPlayerAuthraizedRequest
    {
        public Guid? PlayerId { get; private set; }
        public GetPhysicalCapacityOverTimeQuery(Guid? playerId)
        {
            PlayerId = playerId;
        }
        Guid? IPlayerAuthraizedRequest.PlayerId
        {
            get => PlayerId;
            set => PlayerId = value.Value;
        }
    }


    public class PhysicalCapacityDataReadModel
    {
        public DateTime CreatedAt { get; set; }
        public double CalculatedCapacity { get; set; }
        public double CalculatedExplosivePowerCapacity { get; set; }
        public double CalculatedAerobicCapacity { get; set; }
        public double CalculatedSpeedCapacity { get; set; }
        public decimal ProgressFromLastTest { get; set; }
        public decimal? OverriddenCapacity { get; set; }
    }

}
