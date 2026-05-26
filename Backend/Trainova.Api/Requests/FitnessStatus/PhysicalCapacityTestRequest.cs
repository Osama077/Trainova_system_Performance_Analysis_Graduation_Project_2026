using System;
using Trainova.Application.FitnessStatus.PhysicalCapacityTests.Commands.CreatePhysicalCapacityTest;
using Trainova.Application.FitnessStatus.PhysicalCapacityTests.Commands.UpdatePhysicalCapacityTest;

namespace Trainova.Api.Requests.FitnessStatus
{
    public class CreatePhysicalCapacityTestRequest
    {
        public Guid PlayerId { get; set; }
        public decimal MaximumOxygenConsumption { get; set; }
        public int YoYoIntermittentRecoveryLevel1Distance { get; set; }
        public int YoYoIntermittentRecoveryLevel2Distance { get; set; }
        public decimal Time10Meters { get; set; }
        public decimal Time30Meters { get; set; }
        public decimal CountermovementJumpHeight { get; set; }
        public decimal ReactiveStrengthIndex { get; set; }

        public CreatePhysicalCapacityTestCommand ToCommand()
        {
            return new CreatePhysicalCapacityTestCommand(
                PlayerId: PlayerId,
                MaximumOxygenConsumption: MaximumOxygenConsumption,
                YoYoIntermittentRecoveryLevel1Distance: YoYoIntermittentRecoveryLevel1Distance,
                YoYoIntermittentRecoveryLevel2Distance: YoYoIntermittentRecoveryLevel2Distance,
                Time10Meters: Time10Meters,
                Time30Meters: Time30Meters,
                CountermovementJumpHeight: CountermovementJumpHeight,
                ReactiveStrengthIndex: ReactiveStrengthIndex
            );
        }
    }

    public class UpdatePhysicalCapacityTestRequest
    {
        public decimal MaximumOxygenConsumption { get; set; }
        public int YoYoIntermittentRecoveryLevel1Distance { get; set; }
        public int YoYoIntermittentRecoveryLevel2Distance { get; set; }
        public decimal Time10Meters { get; set; }
        public decimal Time30Meters { get; set; }
        public decimal CountermovementJumpHeight { get; set; }
        public decimal ReactiveStrengthIndex { get; set; }

        public UpdatePhysicalCapacityTestCommand ToCommand(Guid id)
        {
            return new UpdatePhysicalCapacityTestCommand(
                Id: id,
                MaximumOxygenConsumption: MaximumOxygenConsumption,
                YoYoIntermittentRecoveryLevel1Distance: YoYoIntermittentRecoveryLevel1Distance,
                YoYoIntermittentRecoveryLevel2Distance: YoYoIntermittentRecoveryLevel2Distance,
                Time10Meters: Time10Meters,
                Time30Meters: Time30Meters,
                CountermovementJumpHeight: CountermovementJumpHeight,
                ReactiveStrengthIndex: ReactiveStrengthIndex
            );
        }
    }
}
