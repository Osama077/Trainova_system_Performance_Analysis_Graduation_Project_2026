using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies;

namespace Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity
{
    public interface IUserAccessPolicyRepository
    {
        Task AddAsync(UserAccessPolicy userAccessPolicy);
        Task AddRangeAsync(List<UserAccessPolicy> userAccessPolicies);
    }
}
