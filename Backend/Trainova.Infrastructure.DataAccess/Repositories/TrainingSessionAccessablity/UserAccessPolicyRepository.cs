using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies;
using Trainova.Infrastructure.DataAccess.DbSettingsObjects;

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
        public Task AddAsync(UserAccessPolicy userAccessPolicy)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(List<UserAccessPolicy> userAccessPolicies)
        {
            throw new NotImplementedException();
        }
    }
}
