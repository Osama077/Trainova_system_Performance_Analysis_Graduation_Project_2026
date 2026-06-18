using Trainova.Application.Profiles.Players.Common;
using Trainova.Application.Profiles.Queries.GetPlayersProfiles;

namespace Trainova.Api.Requsts.Profiles
{
    public class GetProfilesFiltrationRequest
    {
        public string? SearchTerm { get; init; }


        public int? PerformanceLevel { get; init; }

        public bool? IsActive { get; init; }

        public int? MainPositionFilter { get; init; }

        public int? OtherPositionFilter { get; init; }

        public DateTime? DateFrom { get; init; }

        public DateTime? DateTo { get; init; }


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
                playerId,
                searchTerm: SearchTerm,
                performanceLevel: PerformanceLevel,
                isActive: IsActive,
                mainPositionFilter: MainPositionFilter,
                otherPositionFilter: OtherPositionFilter,
                dateFrom: DateFrom,
                dateTo: DateTo,
                medicalStatus: MedicalStatus,
                pageNumber: PageNumber,
                pageSize: PageSize,
                sortColumn: SortColumn,
                sortDirection: SortDirection
            );
        }
    }

}
