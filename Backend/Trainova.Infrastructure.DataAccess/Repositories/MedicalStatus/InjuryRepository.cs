using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Domain.MedicalStatus.Injuries;

namespace Trainova.Infrastructure.DataAccess.Repositories.MedicalStatus
{
    public class InjuryRepository : IInjuryRepository
    {
        public Task AddAsync(Injury injury)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Injury injury)
        {
            throw new NotImplementedException();
        }

        public Task<Injury> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Injury>> GetInjuriesAsync(Guid? id, string? injuryType, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Injury injury)
        {
            throw new NotImplementedException();
        }
    }
}
