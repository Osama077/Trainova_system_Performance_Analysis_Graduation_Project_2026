using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Trainova.Application.Common.Interfaces.Repositories;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.MedicalStatus.Injuries.Queries.GetInjuryDetailes;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Infrastructure.DataAccess.Repositories.MedicalStatus
{
    public class InjuryRepository : IInjuryRepository
    {
        private readonly TrainovaWriteDbContext _db;
        private readonly IDbSettings _dbSettings;

        public InjuryRepository(TrainovaWriteDbContext db, IDbSettings dbSettings)
        {
            _db = db;
            _dbSettings = dbSettings;
        }

        public async Task AddAsync(Injury injury)
        {
            await _db.Injuries.AddAsync(injury);
        }

        public async Task DeleteAsync(Injury injury)
        {
            _db.Injuries.Remove(injury);
            await Task.CompletedTask;
        }

        public async Task<Injury?> GetByIdAsync(Guid id)
        {
            return await _db.Injuries.FirstOrDefaultAsync(i => i.Id == id);
        }

        /*
            public async Task<IEnumerable<Injury>> GetInjuriesAsync(Guid? id = null, string? injuryType = null, string? searchTerm = null)
            {
                var query = _db.Injuries.AsQueryable();

                if (id.HasValue)
                    query = query.Where(i => i.Id == id.Value);

                if (!string.IsNullOrWhiteSpace(injuryType))
                    query = query.Where(i => i.InjuryType != null && i.InjuryType.ToString() == injuryType);

                return await query.ToListAsync();
            }
        */
        public async Task<IEnumerable<Injury>> GetInjuriesAsync(Guid? id = null, string? injuryType = null, string? searchTerm = null)
        {
            const string sql = "InjuriesData.sp_GetInjuries";

            var parameters = new
            {
                Id = id,
                InjuryType = injuryType,
                SearchTerm = searchTerm
            };

            using var conn = _dbSettings.CreateReadingConnection();

            return await conn.QueryAsync<Injury>(
                sql: sql,
                param: parameters,
                commandType: CommandType.StoredProcedure
            );
        }


        public async Task<InjuryDetailes?> GetInjyryDetailesAsync(Guid id)
        {
            const string sql = "InjuriesData.sp_GetInjuryDetailesById";
            var param = new { Id = id };
            using var conn = _dbSettings.CreateReadingConnection();

            return await conn.QueryFirstOrDefaultAsync<InjuryDetailes>(
                sql,
                param,
                commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateAsync(Injury injury)
        {
            _db.Injuries.Update(injury);
            await Task.CompletedTask;
        }
    }
}
