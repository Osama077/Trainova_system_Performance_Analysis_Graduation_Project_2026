using Trainova.Application.FitnessStatus.PhysicalCapacityTests.Commands.CreatePhysicalCapacityTest;

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
                playerId: PlayerId,
                maximumOxygenConsumption: MaximumOxygenConsumption,
                yoYoIntermittentRecoveryLevel1Distance: YoYoIntermittentRecoveryLevel1Distance,
                yoYoIntermittentRecoveryLevel2Distance: YoYoIntermittentRecoveryLevel2Distance,
                time10Meters: Time10Meters,
                time30Meters: Time30Meters,
                countermovementJumpHeight: CountermovementJumpHeight,
                reactiveStrengthIndex: ReactiveStrengthIndex
            );
        }
    }


}
