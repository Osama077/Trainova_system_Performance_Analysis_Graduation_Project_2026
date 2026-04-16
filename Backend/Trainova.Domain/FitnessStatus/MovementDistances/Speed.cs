using Microsoft.EntityFrameworkCore;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Domain.FitnessStatus.MovementDistances
{
    [Owned]
    public record Speed
    {
        public Speed(decimal averageSpeed, decimal maxSpeed, decimal peakAcceleration)
        {
            if (averageSpeed < 0)
                throw new DomainException(
                    code:"speed.invalid_average",
                    message: "Invalid input.");

            if (maxSpeed < averageSpeed)
                throw new DomainException(
                    code: "speed.invalid_max",
                    message: "Invalid input.");

            if (peakAcceleration < 0)
                throw new DomainException(
                    code: "speed.invalid_acceleration",
                    message: "Invalid input.");

            AverageSpeed = averageSpeed;
            MaxSpeed = maxSpeed;
            PeakAcceleration = peakAcceleration;
        }


        public decimal AverageSpeed { get; set; }
        public decimal MaxSpeed { get; set; }
        public decimal PeakAcceleration { get; set; }

    }

}
