using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Application.Profiles.Players.Common;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Profiles.Players.Queries.GetPlayersProfiles
{
    [Authorize]
    public record GetPlayersProfileQuery : IRequest<ResultOf<IEnumerable<PlayerDetailResponse>>>,IPlayerAuthraizedRequest
    {
        public Guid? PlayerId { get; private set; }

        Guid? IPlayerAuthraizedRequest.PlayerId
        {
            get => PlayerId;
            set => PlayerId = value;
        }

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

        public string SortColumn { get; init; } = PlayerCommonOptions.CreatedAtSortOption;

        public string SortDirection { get; init; } = "DESC";

        public GetPlayersProfileQuery(
            Guid? playerId = null,
            string? searchTerm = null,
            Guid? teamId = null,
            int? performanceLevel = null,
            bool? isActive = null,
            int? mainPositionFilter = null,
            int? otherPositionFilter = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            int? minMatches = null,
            string? medicalStatus = null,
            int pageNumber = 0,
            int pageSize = 12,
            string sortColumn = PlayerCommonOptions.CreatedAtSortOption,
            string sortDirection = "DESC")
        {
            PlayerId = playerId;
            SearchTerm = searchTerm;
            TeamId = teamId;
            PerformanceLevel = performanceLevel;
            IsActive = isActive;
            MainPositionFilter = mainPositionFilter;
            OtherPositionFilter = otherPositionFilter;
            DateFrom = dateFrom;
            DateTo = dateTo;
            MinMatches = minMatches;
            MedicalStatus = medicalStatus;
            PageNumber = pageNumber;
            PageSize = pageSize;
            SortColumn = sortColumn;
            SortDirection = sortDirection;
        }
    }
}
