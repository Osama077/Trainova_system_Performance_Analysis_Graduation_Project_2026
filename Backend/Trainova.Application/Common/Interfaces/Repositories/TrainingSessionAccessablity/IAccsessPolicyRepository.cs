using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies;

namespace Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity
{
    public interface IAccsessPolicyRepository
    {
        Task AddAsync(AccessPolicy accessPolicy);
        Task<AccessPolicy?> GetByIdAsync(Guid value);
    }
}
