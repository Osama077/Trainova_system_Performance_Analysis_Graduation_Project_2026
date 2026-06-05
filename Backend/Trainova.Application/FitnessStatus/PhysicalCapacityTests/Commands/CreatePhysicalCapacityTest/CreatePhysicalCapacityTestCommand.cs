using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Application.FitnessStatus.PhysicalCapacityTests.Commands.CreatePhysicalCapacityTest
{
    [Authorize(Roles = "FitnessCoach,Player,Internal,Integrated")]
    public record CreatePhysicalCapacityTestCommand : IRequest<ResultOf<PhysicalCapacityTest>>, IPlayerAuthraizedRequest, IPhysicalMachineAuthraizedRequest
    {
        public Guid PlayerId { get; private set; }
        public decimal MaximumOxygenConsumption { get; init; }
        public int YoYoIntermittentRecoveryLevel1Distance { get; init; }
        public int YoYoIntermittentRecoveryLevel2Distance { get; init; }
        public decimal Time10Meters { get; init; }
        public decimal Time30Meters { get; init; }
        public decimal CountermovementJumpHeight { get; init; }
        public decimal ReactiveStrengthIndex { get; init; }
        public CreationType CreationType { get; private set; } = CreationType.Manual;

        Guid? IPlayerAuthraizedRequest.PlayerId { get => PlayerId; set => PlayerId = value!.Value; }
        CreationType IPhysicalMachineAuthraizedRequest.CreationType { get => CreationType.Manual; set => CreationType = value; }


        public CreatePhysicalCapacityTestCommand(
            Guid playerId,
            decimal maximumOxygenConsumption,
            int yoYoIntermittentRecoveryLevel1Distance,
            int yoYoIntermittentRecoveryLevel2Distance,
            decimal time10Meters,
            decimal time30Meters,
            decimal countermovementJumpHeight,
            decimal reactiveStrengthIndex)
        {
            PlayerId = playerId;
            MaximumOxygenConsumption = maximumOxygenConsumption;
            YoYoIntermittentRecoveryLevel1Distance = yoYoIntermittentRecoveryLevel1Distance;
            YoYoIntermittentRecoveryLevel2Distance = yoYoIntermittentRecoveryLevel2Distance;
            Time10Meters = time10Meters;
            Time30Meters = time30Meters;
            CountermovementJumpHeight = countermovementJumpHeight;
            ReactiveStrengthIndex = reactiveStrengthIndex;
        }

    }
}
