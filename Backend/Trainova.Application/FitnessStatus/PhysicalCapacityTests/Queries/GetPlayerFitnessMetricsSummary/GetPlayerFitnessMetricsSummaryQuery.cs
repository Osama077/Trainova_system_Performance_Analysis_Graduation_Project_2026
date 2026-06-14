using FluentValidation;
using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.FitnessStatus.PhysicalCapacityTests.Queries.GetPlayerFitnessMetricsSummary
{


    public record GetPlayerFitnessMetricsSummaryQuery(Guid PlayerId)
        : IRequest<ResultOf<PlayerFitnessMetricsReadModel>>, IPlayerAuthraizedRequest
    {
        public Guid PlayerId { get; private set; } = PlayerId;
        Guid? IPlayerAuthraizedRequest.PlayerId { get { return PlayerId; } set { PlayerId = value.Value; } }
    }

    public class GetPlayerFitnessMetricsSummaryQueryValidator : AbstractValidator<GetPlayerFitnessMetricsSummaryQuery>
    {
        public GetPlayerFitnessMetricsSummaryQueryValidator()
        {
            RuleFor(x => x.PlayerId)
                .NotEmpty()
                .WithMessage("PlayerId must be provided to evaluate fitness progression.");
        }
    }

    public class PlayerFitnessMetricsReadModel
    {
        // Part 1: Metric Progression (Current vs Target or Trend)
        public decimal SpeedScore { get; set; }           // Scale 0-100%
        public decimal EnduranceScore { get; set; }       // Scale 0-100%
        public decimal ExplosivePowerScore { get; set; }  // Scale 0-100%
        public decimal FitnessPerformance { get; set; }   // Calculated from (Capacity Trend / Load Trend)

        // Part 2: Squad Comparison
        public decimal OverallCapacity { get; set; }       // Player Current Capacity
        public decimal SquadAverageCapacity { get; set; }  // Avg Capacity of the team
        public decimal SquadAverageSpeed { get; set; }
        public decimal SquadAverageEndurance { get; set; }
        public decimal SquadAveragePower { get; set; }
        public decimal SquadAveragePerformance { get; set; }
    }
}
