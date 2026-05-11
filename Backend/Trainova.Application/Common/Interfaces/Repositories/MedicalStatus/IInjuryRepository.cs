using Trainova.Application.MedicalStatus.Injuries.Queries.GetInjuryDetailes;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.Common.Interfaces.Repositories.MedicalStatus
{
    public interface IInjuryRepository
    {
        Task AddAsync(Injury injury);
        Task<Injury?> GetByIdAsync(Guid id);
        Task<IEnumerable<Injury>> GetInjuriesAsync(Guid? id = null, string? injuryType = null, string? SearchTerm = null);
        Task UpdateAsync(Injury injury);
        Task DeleteAsync(Injury injury);
        Task<InjuryDetailes?> GetInjyryDetailesAsync(Guid id);
    }
}
