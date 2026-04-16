using Microsoft.EntityFrameworkCore;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Domain.FitnessStatus.MovementDistances
{
    [Owned]
    public record Distance
    {
        private const decimal DefaultHumanErrorPercentage = 5;

        public decimal TotalDistance { get; private set; }
        public decimal WalkDistance { get; private set; }
        public decimal RunDistance { get; private set; }
        public decimal HighSpeedRunDistance { get; private set; }

        private Distance() { }

        public Distance(
            decimal totalDistance,
            decimal walkDistance,
            decimal runDistance,
            decimal highSpeedRunDistance,
            decimal humanError = DefaultHumanErrorPercentage)
        {
            if (totalDistance < 0 ||
                walkDistance < 0 ||
                runDistance < 0 ||
                highSpeedRunDistance < 0)
            {
                throw new DomainException(code:"distance.negative_value");
            }

            var sum = walkDistance + runDistance + highSpeedRunDistance;

            if (!IsWithinTolerance(sum, totalDistance, humanError))
            {
                throw new DomainException(
                    code: "distance.invalid_sum",
                    message: $"Sum ({sum}) must be within ±{humanError}% of total ({totalDistance})"
                );
            }

            TotalDistance = totalDistance;
            WalkDistance = walkDistance;
            RunDistance = runDistance;
            HighSpeedRunDistance = highSpeedRunDistance;
        }

        private static bool IsWithinTolerance(
            decimal value,
            decimal expected,
            decimal tolerancePercentage)
        {
            var min = expected * (1 - tolerancePercentage / 100);
            var max = expected * (1 + tolerancePercentage / 100);

            return value >= min && value <= max;
        }
    }


}
