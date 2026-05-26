using Trainova.Domain.FitnessStatus;

namespace Trainova.Application.Common.Interfaces.Repositories.FitnessStatus
{
    public interface ISessionMovementRepository
    {
        Task AddAsync(SessionMovement movement);
        Task DeleteAsync(SessionMovement movement);
        Task<SessionMovement?> GetByIdAsync(Guid id);
        Task UpdateAsync(SessionMovement movement);
        Task<SessionMovement?> GetByUserAccessPolicyIdAsync(Guid userAccessPolicyId);
    }
}
