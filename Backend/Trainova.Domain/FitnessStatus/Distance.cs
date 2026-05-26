using Microsoft.EntityFrameworkCore;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Domain.FitnessStatus
{
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
            if (walkDistance < 0 ||
                runDistance < 0 ||
                highSpeedRunDistance < 0)
            {
                throw new DomainException(code:"distance.negative_value");
            }

            var sum = walkDistance + runDistance + highSpeedRunDistance;

            TotalDistance = walkDistance + runDistance + highSpeedRunDistance;
            WalkDistance = walkDistance;
            RunDistance = runDistance;
            HighSpeedRunDistance = highSpeedRunDistance;
        }


    }


}
