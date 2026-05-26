using Microsoft.EntityFrameworkCore;
using Trainova.Domain.Common.BaseEntity;

namespace Trainova.Domain.FitnessStatus.PhysicalCapacityTests
{
    public class PhysicalCapacityTest : Entity<Guid>
    {
        public Guid PlayerId { get; private set; }

        public AerobicCapacityTest AerobicCapacityTest { get; private set; }

        public SprintTest SprintTest { get; private set; }

        public ExplosivePowerTest ExplosivePowerTest { get; private set; }

        public PhysicalCapacityTest(
            Guid playerId,
            AerobicCapacityTest aerobicCapacityTest,
            SprintTest sprintTest,
            ExplosivePowerTest explosivePowerTest)
            : base(Guid.NewGuid())
        {
            PlayerId = playerId;
            AerobicCapacityTest = aerobicCapacityTest;
            SprintTest = sprintTest;
            ExplosivePowerTest = explosivePowerTest;
        }

        private PhysicalCapacityTest() : base()
        {
        }

        public void Update(
            AerobicCapacityTest aerobicCapacityTest,
            SprintTest sprintTest,
            ExplosivePowerTest explosivePowerTest)
        {
            AerobicCapacityTest = aerobicCapacityTest;
            SprintTest = sprintTest;
            ExplosivePowerTest = explosivePowerTest;
        }

    }
    [Owned]
    public record SprintTest(
        decimal Time10Meters,
        decimal Time30Meters);

    [Owned]
    public record AerobicCapacityTest(
        decimal MaximumOxygenConsumption,
        int YoYoIntermittentRecoveryLevel1Distance,
        int YoYoIntermittentRecoveryLevel2Distance);

    [Owned]
    public record ExplosivePowerTest(
        decimal CountermovementJumpHeight,
        decimal ReactiveStrengthIndex);




}
