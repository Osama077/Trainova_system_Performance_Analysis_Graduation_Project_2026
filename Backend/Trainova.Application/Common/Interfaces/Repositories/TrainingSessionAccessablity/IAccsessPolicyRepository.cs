using Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Common;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity
{
    public interface IAccessPolicyRepository
    {
        Task AddAsync(AccessPolicy accessPolicy);
        Task<AccessPolicy?> GetByIdAsync(Guid id);
        Task<AccessPolicy?> GetByIdIncludingUsersAsync(Guid id);
        Task UpdateAsync(AccessPolicy accessPolicy);
        Task DeleteAsync(AccessPolicy accessPolicy);
        Task<bool> ExistsAsync(Guid? id = null);
        // Search and return read-model including usage information (whether policy is used by Plans, TrainingSessions)
        Task<IEnumerable<AccessPolicyReadModel>> SearchWithUsageAsync(string? searchTerm = null, int pageNumber = 0, int pageSize = 25);
    }
}
