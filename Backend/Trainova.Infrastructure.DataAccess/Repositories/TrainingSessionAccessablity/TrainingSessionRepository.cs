﻿using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions;
using Trainova.Infrastructure.DataAccess.DbSettingsObjects;
using Microsoft.EntityFrameworkCore;

namespace Trainova.Infrastructure.DataAccess.Repositories.TrainingSessionAccessablity
{
    public class TrainingSessionRepository: ITrainingSessionRepository
    {
        public TrainingSessionRepository(IDbSettings dbSettings, TrainovaWriteDbContext dbContext)
        {
            _dbSettings = dbSettings;
            _dbContext = dbContext;
        }
        private readonly TrainovaWriteDbContext _dbContext;
        private readonly IDbSettings _dbSettings;

        public async Task AddAsync(TrainingSession session)
        {
            await _dbContext.TrainingSessions.AddAsync(session);
        }

        public async Task<TrainingSession?> GetByIdAsync(Guid id)
        {
            return await _dbContext.TrainingSessions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(TrainingSession session)
        {
            _dbContext.TrainingSessions.Update(session);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(TrainingSession session)
        {
            _dbContext.TrainingSessions.Remove(session);
            await Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Guid? planId = null, Guid? accessPolicyId = null)
        {
            var query = _dbContext.TrainingSessions.AsQueryable();

            if (planId.HasValue)
                query = query.Where(x => x.PlanId == planId);

            if (accessPolicyId.HasValue)
                query = query.Where(x => x.AccessPolicyId == accessPolicyId);

            return await query.AnyAsync();
        }
    }
}
