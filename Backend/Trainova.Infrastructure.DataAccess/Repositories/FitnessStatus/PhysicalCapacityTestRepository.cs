using Microsoft.EntityFrameworkCore;
using Trainova.Application.Common.Interfaces.Repositories.FitnessStatus;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Infrastructure.DataAccess.Repositories.FitnessStatus
{
    public class PhysicalCapacityTestRepository : IPhysicalCapacityTestRepository
    {
        private readonly TrainovaWriteDbContext _dbContext;

        public PhysicalCapacityTestRepository(TrainovaWriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(PhysicalCapacityTest test)
        {
            await _dbContext.CapacityTests.AddAsync(test);
        }

        public async Task DeleteAsync(PhysicalCapacityTest test)
        {
            _dbContext.CapacityTests.Remove(test);
            await Task.CompletedTask;
        }

        public async Task<PhysicalCapacityTest?> GetByIdAsync(Guid id)
        {
            return await _dbContext.CapacityTests.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateAsync(PhysicalCapacityTest test)
        {
            _dbContext.CapacityTests.Update(test);
            await Task.CompletedTask;
        }

        public async Task<PhysicalCapacityTest?> GetByPlayerIdAsync(Guid playerId)
        {
            return await _dbContext.CapacityTests.FirstOrDefaultAsync(t => t.PlayerId == playerId);
        }

        public async Task<PhysicalCapacityTest?> GetLatestByPlayerIdAsync(Guid playerId)
        {
            return await _dbContext.CapacityTests
                .Where(m => m.PlayerId == playerId)
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}
