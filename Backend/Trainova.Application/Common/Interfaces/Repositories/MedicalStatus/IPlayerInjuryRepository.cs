using Trainova.Domain.MedicalStatus.PlayerInjuries;

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
    }
}
