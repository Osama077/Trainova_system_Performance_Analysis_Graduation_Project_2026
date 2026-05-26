using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Quereis.GetUserAccessPolicy;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity
{
    public interface IUserAccessPolicyRepository
    {
        Task AddAsync(UserAccessPolicy userAccessPolicy);
        Task AddRangeAsync(List<UserAccessPolicy> userAccessPolicies);
        Task<UserAccessPolicy?> GetByIdAsync(Guid id);
        Task UpdateAsync(UserAccessPolicy userAccessPolicy);
        Task DeleteAsync(UserAccessPolicy userAccessPolicy);
        Task DeleteByPolicyIdAsync(Guid policyId);
        Task<IEnumerable<UserAccessPolicy>> GetAllAsync(Guid polcyId);
        Task DeleteRangeAsync(IEnumerable<UserAccessPolicy> userpolicies);
        Task<IEnumerable<UserAccessDetailes>> GetUserAccessPolicyDetails(Guid policyId);
    }
}
