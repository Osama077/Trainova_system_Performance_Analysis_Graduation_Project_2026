using Microsoft.EntityFrameworkCore;
using Trainova.Application.Common.Interfaces.Repositories;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Infrastructure.DataAccess.Repositories.TrainingSessionAccessablity
{
    public class TrainingSessionRepository : ITrainingSessionRepository
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

        public async Task<int> CountByAccessPolicyIdAsync(Guid accessPolicyId)
        {
            return await _dbContext.TrainingSessions.CountAsync(x => x.AccessPolicyId == accessPolicyId);
        }

        public async Task<IEnumerable<TrainingSession>> GetTrainingSessionsAsync(
            DateTime? from,
            DateTime? to,
            Guid? userId = null,
            Guid? userAccsessPolicyId = null,
            Guid? creatorId = null)
        {
            // 1. Base Query with required date filters
            var query = _dbContext.TrainingSessions
                .Where(x => (!from.HasValue || x.HappenedAt >= from) && (!to.HasValue || x.HappenedAt <= to));

            // 2. Filter by Access Policy if provided
            if (userAccsessPolicyId.HasValue)
            {
                query = query.Where(x => x.AccessPolicy.PolicyUsers.Any(ua => ua.Id == userAccsessPolicyId.Value));
            }

            // 3. Filter by Creator if provided
            if (creatorId.HasValue)
            {
                query = query.Where(x => x.CreatedBy == creatorId.Value);
            }

            // 4. Filter by Specific User if provided
            if (userId.HasValue)
            {
                query = query.Where(x => x.AccessPolicy.PolicyUsers.Any(up => up.UserId == userId.Value));
            }

            return await query.ToListAsync();
        }

        public async Task<TrainingSession> GetByPlanidAsync(Guid policyId)
        {
            return await _dbContext.TrainingSessions.FirstOrDefaultAsync(x => x.AccessPolicyId == policyId);
        }

        public Task<IEnumerable<TrainingSession>> GetTrainingSessionsAsync(DateTime from, DateTime to, Guid? userId = null, Guid? userAccsessPolicyId = null, Guid? creatorId = null)
        {
            throw new NotImplementedException();
        }
    }
}
