using Trainova.Application.Profiles.Players.Common;
using Trainova.Application.Profiles.Players.Queries.GetPlayersProfiles;

namespace Trainova.Api.Requsts.Profiles
{
    public class GetProfilesFiltrationRequest
    {
        public string? SearchTerm { get; init; }

        public Guid? TeamId { get; init; }

        public int? PerformanceLevel { get; init; }

        public bool? IsActive { get; init; }

        public int? MainPositionFilter { get; init; }

        public int? OtherPositionFilter { get; init; }

        public DateTime? DateFrom { get; init; }

        public DateTime? DateTo { get; init; }

        public int? MinMatches { get; init; }

        public string? MedicalStatus { get; init; }

        public int PageNumber { get; init; } = 0;

        public int PageSize { get; init; } = 12;

        public string SortColumn { get; init; } =
            PlayerCommonOptions.CreatedAtSortOption;

        public string SortDirection { get; init; } = "DESC";

        // =========================
        // Mapping
        // =========================

        public GetPlayersProfileQuery ToQuery(Guid? playerId)
        {
            return new GetPlayersProfileQuery(
                PlayerId: playerId,
                SearchTerm: SearchTerm,
                TeamId: TeamId,
                PerformanceLevel: PerformanceLevel,
                IsActive: IsActive,
                MainPositionFilter: MainPositionFilter,
                OtherPositionFilter: OtherPositionFilter,
                DateFrom: DateFrom,
                DateTo: DateTo,
                MinMatches: MinMatches,
                MedicalStatus: MedicalStatus,
                PageNumber: PageNumber,
                PageSize: PageSize,
                SortColumn: SortColumn,
                SortDirection: SortDirection
            );
        }
    }

}
