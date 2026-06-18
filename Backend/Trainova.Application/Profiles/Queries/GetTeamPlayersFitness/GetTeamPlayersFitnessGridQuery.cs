using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Enums;

namespace Trainova.Application.Profiles.Queries.GetTeamPlayersFitness
{
    public record GetTeamPlayersFitnessGridQuery(
        string? SearchName = null,
        Position? Position = null,
        string? FootageStatus = null) : IRequest<ResultOf<IEnumerable<TeamPlayersFitnessResponse>>>;


    public class PlayerFitnessGridRowReadModel
    {
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string PhotoPath { get; set; }
        public string PlayerNumber { get; set; }
        public Position MainPosition { get; set; }
        public string MedicalStatus { get; set; }

        // Physical Capacity Tests Data (Latest)
        public decimal CalculatedCapacity { get; set; }
        public decimal? OverriddenCapacity { get; set; }
        public decimal ProgressFromLastTest { get; set; }
        public DateTime? LatestTestDate { get; set; }

        // Session Movement Data (Latest)
        public int? SprintsCount { get; set; }
        public int? DurationInMinutes { get; set; }
        public decimal PlayerCalculatedLoad { get; set; }
        public decimal? OverriddenLoad { get; set; }
        public decimal LoadRatioFromLastSession { get; set; }
        public decimal FootageLoadToCapacityRatio { get; set; }
        public string FootageStatus { get; set; } // Map straight to Domain String Values
        public decimal? TotalDistance { get; set; }
        public DateTime? LatestSessionDate { get; set; }
    }

    public class TeamPlayersFitnessResponse
    {
        public Position? FilteredPosition { get; set; }
        public IEnumerable<PlayerFitnessGridRowReadModel> PlayersFitnessData { get; set; }
    }

}
