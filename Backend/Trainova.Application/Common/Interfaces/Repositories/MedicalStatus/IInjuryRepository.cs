using Trainova.Domain.MedicalStatus.Injuries;

namespace Trainova.Application.Common.Interfaces.Repositories.MedicalStatus
{
    public interface IInjuryRepository
    {
        Task AddAsync(Injury injury);
        Task<Injury> GetByIdAsync(Guid id);
        Task<IEnumerable<Injury>> GetInjuriesAsync(Guid? id = null, string? injuryType = null);
        Task UpdateAsync(Injury injury);
        Task DeleteAsync(Injury injury);
    }
}
