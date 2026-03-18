using Trainova.Domain.MedicalStatus.Injuries;

namespace Trainova.Application.Common.Interfaces.Repositories.MedicalStatus
{
    public interface IInjuryRepository
    {
        Task AddAsync(Injury injury);
        Task<Injury> GetByIdAsync(Guid id);
        Task<IEnumerable<Injury>> GetInjuriesAsync(Guid? id, string? injuryType, int page, int pageSize);
        Task UpdateAsync(Injury injury);
        Task DeleteAsync(Injury injury);
    }
}
