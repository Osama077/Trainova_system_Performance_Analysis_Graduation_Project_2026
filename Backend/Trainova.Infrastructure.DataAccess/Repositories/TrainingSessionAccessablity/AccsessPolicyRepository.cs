using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Trainova.Application.Common.Interfaces.Repositories;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Common;
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

        public async Task<AccessPolicy?> GetByIdAsync(Guid id)
        {
            return await _dbContext.AccessPolicies.FirstOrDefaultAsync(x => x.Id == id);
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


        public async Task<IEnumerable<AccessPolicyReadModel>> SearchWithUsageAsync(
            string? searchTerm = null,
            int pageNumber = 0,
            int pageSize = 12)
        {
            const string sql = "tsa.sp_SearchForAccessPolicy";
            var paramerters = new
            {
                SearchTerm = searchTerm,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var conn = _dbSettings.CreateReadingConnection();
            return await conn.QueryAsync<AccessPolicyReadModel>(sql, paramerters, commandType: CommandType.StoredProcedure);

        }

        public async Task<AccessPolicy?> GetByIdIncludingUsersAsync(Guid id)
        {
            return await _dbContext.AccessPolicies
                .Include(x => x.PolicyUsers)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
