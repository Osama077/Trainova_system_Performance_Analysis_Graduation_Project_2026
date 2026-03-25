using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.MatchsManagement.Matches;
using Trainova.Domain.Profiles.Players;

namespace Trainova.Domain.MatchsManagement.ComputedFeatures
{
    public class ComputedFeature : Entity<Guid>
    {

        public Guid PlayerId { get; private set; }
        public Player Player { get; private set; }

        public Guid MatchId { get; private set; }
        public Match Match { get; private set; }

        public int TotalPasses { get; private set; }

        public double PassAccuracy { get; private set; }

        public int ProgressivePasses { get; private set; }

        public int PassesUnderPressure { get; private set; }

        public int TotalShots { get; private set; }

        public int ShotsOnTarget { get; private set; }

        public double ShotAccuracy { get; private set; }

        public double TotalXg { get; private set; }

        public double XgPerShot { get; private set; }

        public double AvgPositionX { get; private set; }

        public double AvgPositionY { get; private set; }

        public double PositionDeviation { get; private set; }

        public int TotalPressures { get; private set; }

        public int PressureRegains { get; private set; }

        public double Ppda { get; private set; }

        public double DistanceCovered { get; private set; }

        public int ProgressiveCarries { get; private set; }

        public double CarrySuccessRate { get; private set; }

        public double DribbleSuccessRate { get; private set; }

        public double ActionsPer90 { get; private set; }

        public double ActivityDrop2ndHalf { get; private set; }

        public double AvgSpeed { get; private set; }

        public int SprintCount { get; private set; }

        public int HighIntensityActions { get; private set; }

        public int FoulsCommitted { get; private set; }

        public int FoulsWon { get; private set; }

        public int YellowCards { get; private set; }

        public int RedCards { get; private set; }

        public double BallRetentionRate { get; private set; }

        public double VaepRating { get; private set; }

        public double OffensiveValue { get; private set; }

        public double DefensiveValue { get; private set; }


        private ComputedFeature() : base()
        {
        }

        public ComputedFeature(
            Guid playerId,
            Guid matchId,
            int totalPasses,
            double passAccuracy,
            int progressivePasses,
            int passesUnderPressure,
            int totalShots,
            int shotsOnTarget,
            double shotAccuracy,
            double totalXg,
            double xgPerShot,
            double avgPositionX,
            double avgPositionY,
            double positionDeviation,
            int totalPressures,
            int pressureRegains,
            double ppda,
            double distanceCovered,
            int progressiveCarries,
            double carrySuccessRate,
            double dribbleSuccessRate,
            double actionsPer90,
            double activityDrop2ndHalf,
            double avgSpeed,
            int sprintCount,
            int highIntensityActions,
            int foulsCommitted,
            int foulsWon,
            int yellowCards,
            int redCards,
            double ballRetentionRate,
            double vaepRating,
            double offensiveValue,
            double defensiveValue,
            Guid? createdBy = null) : base(Guid.NewGuid(), createdBy)
        {
            PlayerId = playerId;
            MatchId = matchId;
            TotalPasses = totalPasses;
            PassAccuracy = passAccuracy;
            ProgressivePasses = progressivePasses;
            PassesUnderPressure = passesUnderPressure;
            TotalShots = totalShots;
            ShotsOnTarget = shotsOnTarget;
            ShotAccuracy = shotAccuracy;
            TotalXg = totalXg;
            XgPerShot = xgPerShot;
            AvgPositionX = avgPositionX;
            AvgPositionY = avgPositionY;
            PositionDeviation = positionDeviation;
            TotalPressures = totalPressures;
            PressureRegains = pressureRegains;
            Ppda = ppda;
            DistanceCovered = distanceCovered;
            ProgressiveCarries = progressiveCarries;
            CarrySuccessRate = carrySuccessRate;
            DribbleSuccessRate = dribbleSuccessRate;
            ActionsPer90 = actionsPer90;
            ActivityDrop2ndHalf = activityDrop2ndHalf;
            AvgSpeed = avgSpeed;
            SprintCount = sprintCount;
            HighIntensityActions = highIntensityActions;
            FoulsCommitted = foulsCommitted;
            FoulsWon = foulsWon;
            YellowCards = yellowCards;
            RedCards = redCards;
            BallRetentionRate = ballRetentionRate;
            VaepRating = vaepRating;
            OffensiveValue = offensiveValue;
            DefensiveValue = defensiveValue;
        }
    }

}
