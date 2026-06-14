namespace Trainova.Application.FitnessStatus.PhysicalCapacityTests.Queries.GetPhysicalCapacityTests
{
    public class PlayerPhysicalCapacityTestReadModel
    {
        public Guid TestId { get; set; }
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; }

        // Aerobic Capacity Data
        public decimal MaximumOxygenConsumption { get; set; }
        public int YoYoIntermittentRecoveryLevel1Distance { get; set; }
        public int YoYoIntermittentRecoveryLevel2Distance { get; set; }

        // Sprint Test Data
        public decimal Time10Meters { get; set; }
        public decimal Time30Meters { get; set; }

        // Explosive Power Data
        public decimal CountermovementJumpHeight { get; set; }
        public decimal ReactiveStrengthIndex { get; set; }

        // Metadata & Calculations
        public int CreationType { get; set; } // Map to Enum in Application Level if needed
        public decimal? OverriddenCapacity { get; set; }
        public decimal CalculatedCapacity { get; set; }
        public decimal ProgressFromLastTest { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
