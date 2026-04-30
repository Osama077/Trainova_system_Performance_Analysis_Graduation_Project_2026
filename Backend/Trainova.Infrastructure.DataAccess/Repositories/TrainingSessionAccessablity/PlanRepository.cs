using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Infrastructure.DataAccess.DbSettingsObjects;

namespace Trainova.Infrastructure.DataAccess.Repositories.TrainingSessionAccessablity
{
    internal class PlanRepository : IPlanRepository
    {
        public PlanRepository(IDbSettings dbSettings, TrainovaWriteDbContext dbContext)
        {
            _dbSettings = dbSettings;
            _dbContext = dbContext;
        }
        private readonly TrainovaWriteDbContext _dbContext;
        private readonly IDbSettings _dbSettings;

        public Task<bool> ExistsAsync(Guid value)
        {
            throw new NotImplementedException();
        }
    }
}
