using MediatR;
using Trainova.Application.Profiles.Players.Common;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Profiles.Players.Queries.GetPlayersProfiles
{
    public record GetPlayersProfileQuery(
            Guid? PlayerId = null,
            string SearchTerm = null,
            Guid? TeamId = null,
            int? PerformanceLevel = null,
            bool? IsActive = null,
            int? MainPositionFilter = null,
            int? OtherPositionFilter = null,
            DateTime? DateFrom = null,
            DateTime? DateTo = null,
            int? MinMatches = null,
            string MedicalStatus = null,
            int PageNumber = 0,
            int PageSize = 12,
            string SortColumn = PlayerCommonOptions.CreatedAtSortOption,
            string SortDirection = "DESC")
        : IRequest<ResultOf<IEnumerable<PlayerDetailResponse>>>;
}
