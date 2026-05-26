using Trainova.Domain.FitnessStatus.PhysicalCapacityTests;

namespace Trainova.Application.Common.Interfaces.Repositories.FitnessStatus
{
    public interface IPhysicalCapacityTestRepository
    {
        Task AddAsync(PhysicalCapacityTest test);
        Task DeleteAsync(PhysicalCapacityTest test);
        Task<PhysicalCapacityTest?> GetByIdAsync(Guid id);
        Task UpdateAsync(PhysicalCapacityTest test);
        Task<PhysicalCapacityTest?> GetByPlayerIdAsync(Guid playerId);
    }
}
