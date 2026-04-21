using Trainova.Domain.MedicalStatus.PlayerInjuries;
using Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetPlayerInjuries;

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
            Guid? playerInjuryId = null,
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
            int pageNumber = 1,
            int pageSize = 20,
            string? sortColumn = null,
            string? sortDirection = null
            );
    }
}
