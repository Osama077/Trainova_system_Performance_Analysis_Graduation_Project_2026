using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Profiles.Players;
using Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions;

namespace Trainova.Domain.FitnessStatus.MovementDistances
{
    public class SessionMovement : AuditableEntity<Guid>
    {
        private const decimal DefaultHumanError = 5;

        public Guid PlayerId { get; private set; }
        public Player Player { get; private set; }

        public Guid TrainingSessionId { get; private set; }
        public TrainingSession TrainingSession { get; private set; }

        public int? SprintsCount { get; private set; }

        public Distance? Distance { get; private set; }
        public Speed? Speed { get; private set; }

        public decimal? PlayerLoad { get; private set; }

        private SessionMovement() : base() { }

        public SessionMovement(
            Guid playerId,
            Guid trainingSessionId,
            int sprintsCount,
            Distance? distance,
            Speed? speed,
            decimal playerLoad)
        {
            if (sprintsCount < 0)
                throw new ArgumentException("Invalid sprints count.");

            if (playerLoad < 0)
                throw new ArgumentException("Invalid player load.");

            PlayerId = playerId;
            TrainingSessionId = trainingSessionId;
            SprintsCount = sprintsCount;
            Distance = distance;
            Speed = speed;
            PlayerLoad = playerLoad;
        }

        public static SessionMovement CreateFromRawData(
            Guid playerId,
            Guid trainingSessionId,
            int sprintsCount,
            decimal? averageSpeed,
            decimal? maxSpeed,
            decimal? peakAcceleration,
            decimal playerLoad,
            decimal? totalDistance,
            decimal? walkDistance,
            decimal? runDistance,
            decimal? highSpeedRunDistance,
            decimal? humanError = null)
        {
            var distance = totalDistance.HasValue
                ? new Distance(
                    totalDistance.Value,
                    walkDistance ?? 0,
                    runDistance ?? 0,
                    highSpeedRunDistance ?? 0,
                    humanError ?? DefaultHumanError)
                : null;

            var speed = averageSpeed.HasValue
                ? new Speed(
                    averageSpeed.Value,
                    maxSpeed ?? averageSpeed.Value,
                    peakAcceleration ?? 0)
                : null;

            return new SessionMovement(
                playerId,
                trainingSessionId,
                sprintsCount,
                distance,
                speed,
                playerLoad);
        }
    }


}
