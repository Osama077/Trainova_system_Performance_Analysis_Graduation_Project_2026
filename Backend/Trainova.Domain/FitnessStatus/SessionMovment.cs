using Microsoft.EntityFrameworkCore;
using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Domain.FitnessStatus
{
    public class SessionMovement : AuditableEntity<Guid>
    {
        public Guid UserAccessPolicyId { get; private set; }
        public UserAccessPolicy UserAccessPolicy { get; private set; }
        public int? SprintsCount { get; private set; }
        public int DurationInMinutes { get; private set; }
        public Distance? Distance { get; private set; }
        public Speed? Speed { get; private set; }
        public decimal PlayerCalculatedLoad { get; private set; }
        public decimal? OverriddenLoad { get; private set; }
        public decimal LoadRatioFromLastSession { get; private set; }
        public decimal FootageLoadToCapacityRatio { get; private set; }
        public string FootageStatus { get; private set; }

        private SessionMovement() : base() { }

        public SessionMovement(
            Guid userAccessPolicyId,
            int sprintsCount,
            int durationInMinutes,
            Distance? distance,
            Speed? speed,
            PhysicalCapacityTest? currentTest = null,
            SessionMovement? lastSessionMovement = null,
            Guid? createdBy = null)
            : base(Guid.NewGuid(), createdBy)
        {
            if (sprintsCount < 0)
                throw new DomainException("Invalid sprints count.");

            UserAccessPolicyId = userAccessPolicyId;
            SprintsCount = sprintsCount;
            DurationInMinutes = durationInMinutes > 0 ? durationInMinutes : 1;
            Distance = distance;
            Speed = speed;

            CalculateLoad(currentTest);
            CalculateFootageAndTrends(lastSessionMovement, currentTest);
        }

        public void Update(
            int? sprintsCount,
            int? durationInMinutes,
            Distance? distance,
            Speed? speed,
            PhysicalCapacityTest? currentTest = null,
            SessionMovement? lastSessionMovement = null)
        {
            MarkUpdatedNow();
            if (sprintsCount.HasValue)
            {
                if (sprintsCount.Value < 0)
                    throw new DomainException("Invalid sprints count.");
                SprintsCount = sprintsCount.Value;
            }

            if (durationInMinutes.HasValue && durationInMinutes.Value > 0)
                DurationInMinutes = durationInMinutes.Value;

            if (distance != null)
                Distance = distance;

            if (speed != null)
                Speed = speed;


            CalculateLoad(currentTest);
            CalculateFootageAndTrends(lastSessionMovement, currentTest);
        }


        private void CalculateLoad(PhysicalCapacityTest? lastTest)
        {
            if (Distance == null || Speed == null)
            {
                PlayerCalculatedLoad = 0;
                return;
            }

            const decimal walkWeight = 1.0m;
            const decimal runWeight = 2.5m;
            const decimal hsrWeight = 5.0m;

            decimal speedModifier = 1.0m;
            decimal enduranceModifier = 1.0m;
            decimal powerModifier = 1.0m;

            if (lastTest != null)
            {
                if (lastTest.SprintTest?.Time30Meters > 0)
                {
                    const decimal baselineSprint = 4.0m;
                    speedModifier = lastTest.SprintTest.Time30Meters / baselineSprint;
                }

                if (lastTest.AerobicCapacityTest?.YoYoIntermittentRecoveryLevel1Distance > 0)
                {
                    const decimal baselineYoYo = 2000.0m;
                    enduranceModifier = baselineYoYo / (decimal)lastTest.AerobicCapacityTest.YoYoIntermittentRecoveryLevel1Distance;
                }

                if (lastTest.ExplosivePowerTest?.CountermovementJumpHeight > 0)
                {
                    const decimal baselineJump = 45.0m;
                    powerModifier = baselineJump / lastTest.ExplosivePowerTest.CountermovementJumpHeight;
                }
            }

            decimal finalWalkLoad = Distance.WalkDistance * walkWeight;
            decimal finalRunLoad = Distance.RunDistance * runWeight * enduranceModifier;
            decimal finalHsrLoad = Distance.HighSpeedRunDistance * hsrWeight * enduranceModifier * speedModifier;

            decimal sprintFactor = (SprintsCount ?? 0) * 10.0m * speedModifier * powerModifier;
            decimal accelerationFactor = Speed.PeakAcceleration * 1.5m * powerModifier;

            decimal totalBiomechanicalEffort = finalWalkLoad + finalRunLoad + finalHsrLoad + sprintFactor + accelerationFactor;

            PlayerCalculatedLoad = Math.Round((totalBiomechanicalEffort / DurationInMinutes) * 0.1m, 2);
        }

        private void CalculateFootageAndTrends(SessionMovement? lastSessionMovement, PhysicalCapacityTest? currentTest)
        {
            decimal currentEffectiveLoad = OverriddenLoad ?? PlayerCalculatedLoad;

            if (lastSessionMovement == null || lastSessionMovement.PlayerCalculatedLoad == 0)
            {
                LoadRatioFromLastSession = 1.0m;
                FootageLoadToCapacityRatio = 1.0m;
                FootageStatus = "Baseline Status";
                return;
            }

            decimal lastEffectiveLoad = lastSessionMovement.OverriddenLoad ?? lastSessionMovement.PlayerCalculatedLoad;
            LoadRatioFromLastSession = Math.Round(currentEffectiveLoad / lastEffectiveLoad, 2);

            decimal capacityTrend = currentTest != null ? (currentTest.OverriddenCapacity ?? currentTest.CalculatedCapacity) : 1.0m;

            FootageLoadToCapacityRatio = Math.Round(LoadRatioFromLastSession / capacityTrend, 2);

            bool isLoadIncreasing = LoadRatioFromLastSession > 1.0m;
            bool isCapacityIncreasing = capacityTrend >= 1.0m;

            if (isLoadIncreasing && isCapacityIncreasing)
                FootageStatus = "Positive Adaptation";
            else if (isLoadIncreasing && !isCapacityIncreasing)
                FootageStatus = "Maladaptation Risk";
            else if (!isLoadIncreasing && isCapacityIncreasing)
                FootageStatus = "Over-Recovered";
            else
                FootageStatus = "Detraining Status";
        }
    }

    [Owned]
    public record Distance
    {
        public decimal TotalDistance { get; private set; }
        public decimal WalkDistance { get; private set; }
        public decimal RunDistance { get; private set; }
        public decimal HighSpeedRunDistance { get; private set; }

        private Distance() { }

        public Distance(
            decimal walkDistance,
            decimal runDistance,
            decimal highSpeedRunDistance)
        {
            if (walkDistance < 0 || runDistance < 0 || highSpeedRunDistance < 0)
            {
                throw new DomainException(code: "distance.negative_value");
            }

            TotalDistance = walkDistance + runDistance + highSpeedRunDistance;
            WalkDistance = walkDistance;
            RunDistance = runDistance;
            HighSpeedRunDistance = highSpeedRunDistance;
        }
    }

    [Owned]
    public record Speed
    {
        public decimal AverageSpeed { get; set; }
        public decimal MaxSpeed { get; set; }
        public decimal PeakAcceleration { get; set; }

        public Speed(decimal averageSpeed, decimal maxSpeed, decimal peakAcceleration)
        {
            if (averageSpeed < 0)
                throw new DomainException(code: "speed.invalid_average", message: "Invalid input.");

            if (maxSpeed < averageSpeed)
                throw new DomainException(code: "speed.invalid_max", message: "Invalid input.");

            if (peakAcceleration < 0)
                throw new DomainException(code: "speed.invalid_acceleration", message: "Invalid input.");

            AverageSpeed = averageSpeed;
            MaxSpeed = maxSpeed;
            PeakAcceleration = peakAcceleration;
        }
    }
}