using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies;
using Trainova.Infrastructure.DataAccess.DbSettingsObjects;

namespace Trainova.Infrastructure.DataAccess.Repositories.TrainingSessionAccessablity
{
    internal class AccsessPolicyRepository : IAccsessPolicyRepository
    {


        public AccsessPolicyRepository(IDbSettings dbSettings, TrainovaWriteDbContext dbContext)
        {
            _dbSettings = dbSettings;
            _dbContext = dbContext;
        }
        private readonly TrainovaWriteDbContext _dbContext;
        private readonly IDbSettings _dbSettings;
        public Task AddAsync(AccessPolicy accessPolicy)
        {
            throw new NotImplementedException();
        }

        public Task<AccessPolicy?> GetByIdAsync(Guid value)
        {
            throw new NotImplementedException();
        }
    }
}
