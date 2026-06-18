using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity
{
    public interface ITrainingSessionRepository
    {
        Task AddAsync(TrainingSession session);
        Task<TrainingSession?> GetByIdAsync(Guid id);
        Task UpdateAsync(TrainingSession session);
        Task DeleteAsync(TrainingSession session);
        Task<bool> ExistsAsync(Guid? planId = null, Guid? accessPolicyId = null);
        Task<int> CountByAccessPolicyIdAsync(Guid accessPolicyId);
        Task<IEnumerable<TrainingSession>> GetTrainingSessionsAsync(DateTime? from, DateTime? to, Guid? userId = null, Guid? userAccsessPolicyId = null, Guid? creatorId = null);
        Task<TrainingSession> GetByPlanidAsync(Guid policyId);
    }
}
