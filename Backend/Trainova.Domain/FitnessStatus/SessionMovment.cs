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

        public Distance? Distance { get; private set; }
        public Speed? Speed { get; private set; }

        public decimal? PlayerLoad { get; private set; }

        private SessionMovement() : base() { }

        public SessionMovement(
            Guid userAccessPolicyId,
            int sprintsCount,
            Distance? distance,
            Speed? speed)
        {
            if (sprintsCount < 0)
                throw new DomainException("Invalid sprints count.");

            SprintsCount = sprintsCount;
            Distance = distance;
            Speed = speed;
            PlayerLoad = 0;// for now but should be implemnted later
        }

        public static SessionMovement CreateFromRawData(
            Guid userAccessPolicyId,
            int sprintsCount,
            decimal? averageSpeed,
            decimal? maxSpeed,
            decimal? peakAcceleration,
            decimal? playerLoad,
            decimal? walkDistance,
            decimal? runDistance,
            decimal? highSpeedRunDistance)
        {
            var distance = walkDistance.HasValue
                ? new Distance(
                    walkDistance ?? 0,
                    runDistance ?? 0,
                    highSpeedRunDistance ?? 0)
                : null;

            var speed = averageSpeed.HasValue
                ? new Speed(
                    averageSpeed.Value,
                    maxSpeed ?? averageSpeed.Value,
                    peakAcceleration ?? 0)
                : null;

            return new SessionMovement(
                userAccessPolicyId,
                sprintsCount,
                distance,
                speed);
        }
    }


}
