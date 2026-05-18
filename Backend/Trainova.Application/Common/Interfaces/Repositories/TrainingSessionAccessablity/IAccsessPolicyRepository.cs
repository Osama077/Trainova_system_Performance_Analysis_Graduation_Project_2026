using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Domain.TrainingSessionsAccessibility;
using Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Common;

namespace Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity
{
    public interface IAccessPolicyRepository
    {
        Task AddAsync(AccessPolicy accessPolicy);
        Task<AccessPolicy?> GetByIdAsync(Guid value);
        Task UpdateAsync(AccessPolicy accessPolicy);
        Task DeleteAsync(AccessPolicy accessPolicy);
        Task<bool> ExistsAsync(Guid? id = null);
        // Search access policies by name with pagination and sorting
        Task<IEnumerable<AccessPolicy>> SearchAsync(string? searchTerm = null, int pageNumber = 0, int pageSize = 25, string? sortColumn = null, string? sortDirection = null);
        // Search and return read-model including usage information (whether policy is used by Plans, TrainingSessions)
        Task<IEnumerable<AccessPolicyReadModel>> SearchWithUsageAsync(string? searchTerm = null, string? usageType = null, int pageNumber = 0, int pageSize = 25, string? sortColumn = null, string? sortDirection = null);
    }
}
