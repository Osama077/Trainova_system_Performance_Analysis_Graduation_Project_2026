using Microsoft.EntityFrameworkCore;
using Trainova.Application.Common.Interfaces.Repositories.FitnessStatus;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Infrastructure.DataAccess.Repositories.FitnessStatus
{
    public class SessionMovementRepository : ISessionMovementRepository
    {
        private readonly TrainovaWriteDbContext _dbContext;

        public SessionMovementRepository(TrainovaWriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(SessionMovement movement)
        {
            await _dbContext.SessionMovements.AddAsync(movement);
        }

        public async Task DeleteAsync(SessionMovement movement)
        {
            _dbContext.SessionMovements.Remove(movement);
            await Task.CompletedTask;
        }

        public async Task<SessionMovement?> GetByIdAsync(Guid id)
        {
            return await _dbContext.SessionMovements.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateAsync(SessionMovement movement)
        {
            _dbContext.SessionMovements.Update(movement);
            await Task.CompletedTask;
        }

        public async Task<SessionMovement?> GetByUserAccessPolicyIdAsync(Guid userAccessPolicyId)
        {
            return await _dbContext.SessionMovements.FirstOrDefaultAsync(m => m.UserAccessPolicyId == userAccessPolicyId);
        }

        public async Task<SessionMovement?> GetLastByUserAccessPolicyIdAsync(Guid playerId)
        {
            return await _dbContext.SessionMovements
                .Where(m => m.UserAccessPolicy.UserId == playerId)
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}
