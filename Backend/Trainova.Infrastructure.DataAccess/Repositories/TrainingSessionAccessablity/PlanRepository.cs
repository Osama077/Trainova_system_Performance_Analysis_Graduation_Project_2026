﻿using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Domain.TrainingSessionsAccessibility.Plans;
using Trainova.Infrastructure.DataAccess.DbSettingsObjects;
using Microsoft.EntityFrameworkCore;

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

        public async Task AddAsync(Plan plan)
        {
            await _dbContext.Plans.AddAsync(plan);
        }

        public async Task<Plan?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Plans.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Plan plan)
        {
            _dbContext.Plans.Update(plan);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Plan plan)
        {
            _dbContext.Plans.Remove(plan);
            await Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Guid? id = null, Guid? accessPolicyId = null)
        {
            var query = _dbContext.Plans.AsQueryable();

            if (id.HasValue)
                query = query.Where(x => x.Id == id);

            if (accessPolicyId.HasValue)
                query = query.Where(x => x.AccessPolicyId == accessPolicyId);

            return await query.AnyAsync();
        }
    }
}
