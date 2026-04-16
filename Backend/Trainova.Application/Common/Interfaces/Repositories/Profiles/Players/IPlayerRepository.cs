using Trainova.Application.Profiles.Players;
using Trainova.Application.Profiles.Players.Common;
using Trainova.Domain.Profiles.Players;

namespace Trainova.Application.Common.Interfaces.Repositories.Profiles.Players
{
    public interface IPlayerRepository
    {
        Task AddAsync(Player player);
        Task UpdateAsync(Player player);
        Task<IEnumerable<PlayerDetailResponse>> GetPlayersAsync(
            Guid? playerId = null,
            string searchTerm = null,
            Guid? teamId = null,
            int? performanceLevel = null,
            bool? isActive = null,
            int? mainPositionFilter = null,
            int? otherPositionFilter = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            int? minMatches = null,
            string medicalStatus = null,
            int pageNumber = 0,
            int pageSize = 12,
            string sortColumn = PlayerCommonOptions.CreatedAtSortOption,
            string sortDirection = "DESC");

    }

}
