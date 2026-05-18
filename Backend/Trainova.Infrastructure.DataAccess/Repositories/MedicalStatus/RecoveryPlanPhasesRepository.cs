using Microsoft.EntityFrameworkCore;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Infrastructure.DataAccess.Repositories.MedicalStatus
{
    public class RecoveryPlanPhasesRepository : IRecoveryPlanPhasesRepository
    {
        private readonly TrainovaWriteDbContext _dbContext;

        public RecoveryPlanPhasesRepository(TrainovaWriteDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddAsync(RecoveryPlanPhase phase)
        {
            await _dbContext.AddAsync(phase);
        }

        public async Task<IEnumerable<RecoveryPlanPhase>> GetByPlayerInjuryIdAsync(Guid playerInjuryId)
        {
            return await _dbContext.PlanPhases.Where(pp=>pp.PlayerInjuryId == playerInjuryId).ToListAsync();
        }

    }
}
