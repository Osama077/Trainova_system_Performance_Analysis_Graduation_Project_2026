using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Domain.MedicalStatus.Injuries;
using Microsoft.EntityFrameworkCore;

namespace Trainova.Infrastructure.DataAccess.Repositories.MedicalStatus
{
    public class InjuryRepository : IInjuryRepository
    {
        private readonly TrainovaWriteDbContext _db;

        public InjuryRepository(TrainovaWriteDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Injury injury)
        {
            await _db.Injuries.AddAsync(injury);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Injury injury)
        {
            _db.Injuries.Remove(injury);
            await _db.SaveChangesAsync();
        }

        public async Task<Injury> GetByIdAsync(Guid id)
        {
            var injury = await _db.Injuries.FindAsync(id);
            if (injury == null)
                throw new KeyNotFoundException($"Injury with id {id} not found");
            return injury;
        }

        public async Task<IEnumerable<Injury>> GetInjuriesAsync(Guid? id, string? injuryType)
        {
            var query = _db.Injuries.AsQueryable();

            if (id.HasValue)
                query = query.Where(i => i.Id == id.Value);

            if (!string.IsNullOrWhiteSpace(injuryType))
                query = query.Where(i => i.InjuryType != null && i.InjuryType.ToString() == injuryType);


            return await query.ToListAsync();
        }

        public async Task UpdateAsync(Injury injury)
        {
            _db.Injuries.Update(injury);
            await _db.SaveChangesAsync();
        }
    }
}
