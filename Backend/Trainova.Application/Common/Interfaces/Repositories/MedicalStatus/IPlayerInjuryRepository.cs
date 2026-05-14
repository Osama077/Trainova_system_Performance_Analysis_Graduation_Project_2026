using Trainova.Application.Common.Helpers;
using Trainova.Application.MedicalStatus.PlayerInjuries;
using Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetCasesCount;
using Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetPlayerInjuries;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.Common.Interfaces.Repositories.MedicalStatus
{
    public interface IPlayerInjuryRepository
    {
        Task DeleteRangeAsync(IEnumerable<PlayerInjury> playerInjuries);
        Task UpdateAsync(PlayerInjury playerInjury);
        Task AddAsync(PlayerInjury playerInjury);

        Task<IEnumerable<PlayerInjury>> GetAllAsync(
            Guid? playerInjuryId = null,
            Guid? playerId = null,
            Guid? injuryId = null,
            InjuryStatus? status = null,
            InjuryCause? cause = null,
            bool? isNew = null,
            DateTime? happendBefore = null,
            DateTime? happendAfter = null,
            DateTime? expectedReturnBefore = null,
            DateTime? expectedReturnAfter = null,
            DateTime? returnedBefore = null,
            DateTime? returnedAfter = null
            );

        Task<IEnumerable<PlayerInjuryReadModel>> GetReadModelsAsync(
            Guid? playerId = null,
            Guid? injuryId = null,
            string? status = null,
            string? cause = null,
            bool? isNew = null,
            DateTime? happendBefore = null,
            DateTime? happendAfter = null,
            DateTime? expectedReturnBefore = null,
            DateTime? expectedReturnAfter = null,
            DateTime? returnedBefore = null,
            DateTime? returnedAfter = null,
            int pageNumber = 0,
            int pageSize = 12,
            string sortColumn = PlayerInjuryCommonOptions.CreatedAtSortOption,
            string sortDirection = GeneralSortHelper.DESCSortOption
            );
        Task<bool> ExistesAsync(
            Guid? playerInjuryId = null,
            Guid? playerId = null,
            Guid? injuryId = null);
        Task<CasesCountResponse> GetInjuriesCountOver(int days = 7, Guid? injuryId = null);
    }
}
