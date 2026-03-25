using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.MatchsManagement.Matches;
using Trainova.Domain.Profiles.Players;

namespace Trainova.Domain.SeasonsManagement.ModelScores
{
    public class ModelScore : Entity<Guid>
    {
        public Guid PlayerId { get; private set; }
        public Player Player { get; private set; }

        public Guid MatchId { get; private set; }
        public Match Match { get; private set; }

        public double PassingScore { get; private set; }

        public double ShootingScore { get; private set; }

        public double PositioningScore { get; private set; }

        public double PressingScore { get; private set; }

        public double MovementScore { get; private set; }

        public double PhysicalScore { get; private set; }

        public double BehavioralScore { get; private set; }

        public double OverallScore { get; private set; }

        public double PositionFitScore { get; private set; }

        public double PercentileInTeam { get; private set; }

        public double PercentileInLeague { get; private set; }

        public double PercentileInPosition { get; private set; }

        public string? PlayerCluster { get; private set; }

        public string? PerformanceTrend { get; private set; }


        private ModelScore() : base()
        {
        }

        public ModelScore(
            Guid playerId,
            Guid matchId,
            double passingScore,
            double shootingScore,
            double positioningScore,
            double pressingScore,
            double movementScore,
            double physicalScore,
            double behavioralScore,
            double overallScore,
            double positionFitScore,
            double percentileInTeam,
            double percentileInLeague,
            double percentileInPosition,
            string? playerCluster,
            string? performanceTrend,
            Guid? createdBy = null) : base(Guid.NewGuid(), createdBy)
        {
            PlayerId = playerId;
            MatchId = matchId;
            PassingScore = passingScore;
            ShootingScore = shootingScore;
            PositioningScore = positioningScore;
            PressingScore = pressingScore;
            MovementScore = movementScore;
            PhysicalScore = physicalScore;
            BehavioralScore = behavioralScore;
            OverallScore = overallScore;
            PositionFitScore = positionFitScore;
            PercentileInTeam = percentileInTeam;
            PercentileInLeague = percentileInLeague;
            PercentileInPosition = percentileInPosition;
            PlayerCluster = playerCluster;
            PerformanceTrend = performanceTrend;
        }
    }

}
