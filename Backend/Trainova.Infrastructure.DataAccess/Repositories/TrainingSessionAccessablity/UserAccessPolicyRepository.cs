using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Trainova.Application.Common.Interfaces.Repositories;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Quereis.GetUserAccessPolicy;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Infrastructure.DataAccess.Repositories.TrainingSessionAccessablity
{
    public class UserAccessPolicyRepository : IUserAccessPolicyRepository
    {
        public UserAccessPolicyRepository(IDbSettings dbSettings, TrainovaWriteDbContext dbContext)
        {
            _dbSettings = dbSettings;
            _dbContext = dbContext;
        }
        private readonly TrainovaWriteDbContext _dbContext;
        private readonly IDbSettings _dbSettings;

        public async Task AddAsync(UserAccessPolicy userAccessPolicy)
        {
            await _dbContext.UserAccessPolicies.AddAsync(userAccessPolicy);
        }

        public async Task AddRangeAsync(List<UserAccessPolicy> userAccessPolicies)
        {
            await _dbContext.UserAccessPolicies.AddRangeAsync(userAccessPolicies);
        }

        public async Task<UserAccessPolicy?> GetByIdAsync(Guid id)
        {
            return await _dbContext.UserAccessPolicies.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(UserAccessPolicy userAccessPolicy)
        {
            _dbContext.UserAccessPolicies.Update(userAccessPolicy);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(UserAccessPolicy userAccessPolicy)
        {
            _dbContext.UserAccessPolicies.Remove(userAccessPolicy);
            await Task.CompletedTask;
        }

        public async Task DeleteByPolicyIdAsync(Guid policyId)
        {
            var userAccessPolicies = await _dbContext.UserAccessPolicies
                .Where(x => x.AccessPoliciesId == policyId)
                .ToListAsync();

            _dbContext.UserAccessPolicies.RemoveRange(userAccessPolicies);
        }

        public async Task<IEnumerable<UserAccessPolicy>> GetAllAsync(Guid policyId)
        {
            return await _dbContext.UserAccessPolicies
                .Where(x => x.AccessPoliciesId == policyId)
                .ToListAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<UserAccessPolicy> userpolicies)
        {
            _dbContext.RemoveRange(userpolicies);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<UserAccessDetailes>> GetUserAccessPolicyDetails(Guid policyId)
        {
            const string sql = "tsa.sp_GetUserAccessPolicyDetails";

            var parameters = new { PolicyId = policyId };


            using var conn = _dbSettings.CreateReadingConnection();



            return await conn.QueryAsync<UserAccessDetailes>(
                sql,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
