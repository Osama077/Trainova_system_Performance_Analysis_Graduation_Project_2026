using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Infrastructure.DataAccess.DbSettingsObjects;
using Microsoft.EntityFrameworkCore;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Infrastructure.DataAccess.Repositories.TrainingSessionAccessablity
{
    internal class AccsessPolicyRepository : IAccessPolicyRepository
    {
        public AccsessPolicyRepository(IDbSettings dbSettings, TrainovaWriteDbContext dbContext)
        {
            _dbSettings = dbSettings;
            _dbContext = dbContext;
        }
        private readonly TrainovaWriteDbContext _dbContext;
        private readonly IDbSettings _dbSettings;

        public async Task AddAsync(AccessPolicy accessPolicy)
        {
            await _dbContext.AccessPolicies.AddAsync(accessPolicy);
        }

        public async Task<AccessPolicy?> GetByIdAsync(Guid value)
        {
            return await _dbContext.AccessPolicies.FirstOrDefaultAsync(x => x.Id == value);
        }

        public async Task UpdateAsync(AccessPolicy accessPolicy)
        {
            _dbContext.AccessPolicies.Update(accessPolicy);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(AccessPolicy accessPolicy)
        {
            _dbContext.AccessPolicies.Remove(accessPolicy);
            await Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Guid? id = null)
        {
            var query = _dbContext.AccessPolicies.AsQueryable();

            if (id.HasValue)
                query = query.Where(x => x.Id == id);

            return await query.AnyAsync();
        }
    }
}
