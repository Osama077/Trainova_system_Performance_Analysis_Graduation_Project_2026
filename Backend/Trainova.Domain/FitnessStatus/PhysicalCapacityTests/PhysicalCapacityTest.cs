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
