using Microsoft.EntityFrameworkCore;
using Trainova.Domain.Common.BaseEntity;

namespace Trainova.Domain.FitnessStatus
{
    public class PhysicalCapacityTest : Entity<Guid>, IMultiWayCreatable
    {
        public Guid PlayerId { get; private set; }

        public AerobicCapacityTest AerobicCapacityTest { get; private set; }

        public SprintTest SprintTest { get; private set; }

        public ExplosivePowerTest ExplosivePowerTest { get; private set; }
        public CreationType CreationType { get; private set; }
        public decimal? OverriddenCapacity { get; private set; }
        public decimal CalculatedCapacity { get; private set; }
        public decimal ProgressFromLastTest { get; private set; }
        public PhysicalCapacityTest(
                    Guid playerId,
                    AerobicCapacityTest aerobicCapacityTest,
                    SprintTest sprintTest,
                    ExplosivePowerTest explosivePowerTest,
                    PhysicalCapacityTest? lastPhysicalCapacityTest = null,
                    CreationType creationType = CreationType.Manual,
                    Guid? createdBy = null)
                    : base(Guid.NewGuid(), createdBy)
        {
            CreationType = creationType;
            PlayerId = playerId;
            AerobicCapacityTest = aerobicCapacityTest;
            SprintTest = sprintTest;
            ExplosivePowerTest = explosivePowerTest;

            const decimal baselineVO2Max = 60.0m;
            const decimal baseline10m = 1.75m;
            const decimal baseline30m = 4.0m;
            const decimal baselineJump = 45.0m;
            const decimal baselineRSI = 2.0m;

            decimal aerobicScore = aerobicCapacityTest.MaximumOxygenConsumption / baselineVO2Max;

            decimal speedScore = 0.0m;
            if (sprintTest.Time10Meters > 0 && sprintTest.Time30Meters > 0)
            {
                decimal score10m = baseline10m / sprintTest.Time10Meters;
                decimal score30m = baseline30m / sprintTest.Time30Meters;
                speedScore = (score10m + score30m) / 2.0m;
            }

            decimal powerScore = 0.0m;
            if (explosivePowerTest.CountermovementJumpHeight > 0 && explosivePowerTest.ReactiveStrengthIndex > 0)
            {
                decimal jumpScore = explosivePowerTest.CountermovementJumpHeight / baselineJump;
                decimal rsiScore = explosivePowerTest.ReactiveStrengthIndex / baselineRSI;
                powerScore = (jumpScore + rsiScore) / 2.0m;
            }

            CalculatedCapacity = Math.Round(
                (aerobicScore * 0.5m) +
                (speedScore * 0.3m) +
                (powerScore * 0.2m), 2);

            if (lastPhysicalCapacityTest is not null && lastPhysicalCapacityTest.CalculatedCapacity > 0)
            {
                ProgressFromLastTest = CalculatedCapacity / lastPhysicalCapacityTest.CalculatedCapacity;

                OverriddenCapacity = lastPhysicalCapacityTest.OverriddenCapacity.HasValue
                    ? lastPhysicalCapacityTest.OverriddenCapacity.Value * ProgressFromLastTest
                    : lastPhysicalCapacityTest.CalculatedCapacity * ProgressFromLastTest;
            }
            else
            {
                ProgressFromLastTest = 1.0m;
                OverriddenCapacity = CalculatedCapacity;
            }
        }

        private PhysicalCapacityTest() : base()
        {
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
