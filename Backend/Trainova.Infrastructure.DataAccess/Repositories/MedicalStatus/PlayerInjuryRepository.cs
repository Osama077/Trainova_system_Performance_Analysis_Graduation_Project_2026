using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Domain.MedicalStatus.PlayerInjuries;

namespace Trainova.Infrastructure.DataAccess.Repositories.MedicalStatus
{
    public class PlayerInjuryRepository : IPlayerInjuryRepository
    {
        public Task AddAsync(PlayerInjury playerInjury)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(IEnumerable<PlayerInjury> playerInjuries)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlayerInjury>> GetAllAsync(Guid? playerInjuryId = null, Guid? playerId = null, Guid? injuryId = null, InjuryStatus? status = null, InjuryCause? cause = null, bool? isNew = null, DateTime? happendBefore = null, DateTime? happendAfter = null, DateTime? expectedReturnBefore = null, DateTime? expectedReturnAfter = null, DateTime? returnedBefore = null, DateTime? returnedAfter = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(PlayerInjury playerInjury)
        {
            throw new NotImplementedException();
        }
    }
}
