using Trainova.Domain.Common.BaseEntity;

namespace Trainova.Domain.SeasonsAnalyses.PositionBenchmarks
{
    public class PositionBenchmark : Entity<Guid>
    {
        public string? PositionGroup { get; private set; }

        public double? AvgPassAccuracy { get; private set; }

        public double? AvgProgressivePasses { get; private set; }

        public double? AvgPressures { get; private set; }

        public double? AvgDistanceCovered { get; private set; }

        public double? AvgXg { get; private set; }

        public double? AvgVaep { get; private set; }

        public double? WeightPassing { get; private set; }

        public double? WeightShooting { get; private set; }

        public double? WeightPositioning { get; private set; }

        public double? WeightPressing { get; private set; }

        public double? WeightMovement { get; private set; }

        public double? WeightPhysical { get; private set; }

        public double? WeightBehavioral { get; private set; }

        public int SeasonId { get; private set; }

        public int CompetitionId { get; private set; }


        private PositionBenchmark() :base() { }

        public PositionBenchmark(
            string? positionGroup,
            double? avgPassAccuracy,
            double? avgProgressivePasses,
            double? avgPressures,
            double? avgDistanceCovered,
            double? avgXg,
            double? avgVaep,
            double? weightPassing,
            double? weightShooting,
            double? weightPositioning,
            double? weightPressing,
            double? weightMovement,
            double? weightPhysical,
            double? weightBehavioral,
            int seasonId,
            int competitionId,
            Guid?createdBy = null) :base (createdBy)
        {
            PositionGroup = positionGroup;
            AvgPassAccuracy = avgPassAccuracy;
            AvgProgressivePasses = avgProgressivePasses;
            AvgPressures = avgPressures;
            AvgDistanceCovered = avgDistanceCovered;
            AvgXg = avgXg;
            AvgVaep = avgVaep;

            WeightPassing = weightPassing;
            WeightShooting = weightShooting;
            WeightPositioning = weightPositioning;
            WeightPressing = weightPressing;
            WeightMovement = weightMovement;
            WeightPhysical = weightPhysical;
            WeightBehavioral = weightBehavioral;

            SeasonId = seasonId;
            CompetitionId = competitionId;

        }
        /*
        public void Update(
            string? positionGroup = null,
            double? avgPassAccuracy = null,
            double? avgProgressivePasses = null,
            double? avgPressures = null,
            double? avgDistanceCovered = null,
            double? avgXg = null,
            double? avgVaep = null,
            double? weightPassing = null,
            double? weightShooting = null,
            double? weightPositioning = null,
            double? weightPressing = null,
            double? weightMovement = null,
            double? weightPhysical = null,
            double? weightBehavioral = null,
            int? seasonId = null,
            int? competitionId = null)
        {
            PositionGroup = positionGroup ?? PositionGroup;

            AvgPassAccuracy = avgPassAccuracy ?? AvgPassAccuracy;
            AvgProgressivePasses = avgProgressivePasses ?? AvgProgressivePasses;
            AvgPressures = avgPressures ?? AvgPressures;
            AvgDistanceCovered = avgDistanceCovered ?? AvgDistanceCovered;
            AvgXg = avgXg ?? AvgXg;
            AvgVaep = avgVaep ?? AvgVaep;

            WeightPassing = weightPassing ?? WeightPassing;
            WeightShooting = weightShooting ?? WeightShooting;
            WeightPositioning = weightPositioning ?? WeightPositioning;
            WeightPressing = weightPressing ?? WeightPressing;
            WeightMovement = weightMovement ?? WeightMovement;
            WeightPhysical = weightPhysical ?? WeightPhysical;
            WeightBehavioral = weightBehavioral ?? WeightBehavioral;

            SeasonId = seasonId ?? SeasonId;
            CompetitionId = competitionId ?? CompetitionId;
        }
        */
    }

}
