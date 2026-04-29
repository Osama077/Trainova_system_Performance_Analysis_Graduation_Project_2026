using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Domain.MedicalStatus.PlayerInjuries;
using Microsoft.EntityFrameworkCore;
using Dapper;
using Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetPlayerInjuries;
using Trainova.Infrastructure.DataAccess.DbSettingsObjects;

namespace Trainova.Infrastructure.DataAccess.Repositories.MedicalStatus
{
    public class PlayerInjuryRepository : IPlayerInjuryRepository
    {
        private readonly TrainovaWriteDbContext _db;
        private readonly IDbSettings _dbSettings;

        public PlayerInjuryRepository(TrainovaWriteDbContext db, IDbSettings dbSettings)
        {
            _db = db;
            _dbSettings = dbSettings;
        }

        public async Task AddAsync(PlayerInjury playerInjury)
        {
            await _db.PlayerInjuries.AddAsync(playerInjury);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<PlayerInjury> playerInjuries)
        {
            _db.PlayerInjuries.RemoveRange(playerInjuries);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlayerInjury>> GetAllAsync(
            Guid? playerInjuryId = null
            , Guid? playerId = null,
            Guid? injuryId = null, 
            InjuryStatus? status = null, 
            InjuryCause? cause = null,
            bool? isNew = null, 
            DateTime? happendBefore = null,
            DateTime? happendAfter = null, 
            DateTime? expectedReturnBefore = null,
            DateTime? expectedReturnAfter = null,
            DateTime? returnedBefore = null,
            DateTime? returnedAfter = null)
        {
            IQueryable<PlayerInjury> query = _db.PlayerInjuries.AsQueryable();

            if (playerInjuryId.HasValue)
                query = query.Where(pi => pi.Id == playerInjuryId.Value);

            if (playerId.HasValue)
                query = query.Where(pi => pi.PlayerId == playerId.Value);

            if (injuryId.HasValue)
                query = query.Where(pi => pi.InjuryId == injuryId.Value);

            if (status.HasValue)
                query = query.Where(pi => pi.Status == status.Value);

            if (cause.HasValue)
                query = query.Where(pi => pi.Cause == cause.Value);

            if (isNew.HasValue)
                query = query.Where(pi => pi.IsNew == isNew.Value);

            if (happendBefore.HasValue)
                query = query.Where(pi => pi.HappendAt <= happendBefore.Value);

            if (happendAfter.HasValue)
                query = query.Where(pi => pi.HappendAt >= happendAfter.Value);

            if (expectedReturnBefore.HasValue)
                query = query.Where(pi => pi.ExpectedReturnDate <= expectedReturnBefore.Value);

            if (expectedReturnAfter.HasValue)
                query = query.Where(pi => pi.ExpectedReturnDate >= expectedReturnAfter.Value);

            if (returnedBefore.HasValue)
                query = query.Where(pi => pi.ReturnedAt <= returnedBefore.Value);

            if (returnedAfter.HasValue)
                query = query.Where(pi => pi.ReturnedAt >= returnedAfter.Value);

            return await query.ToListAsync();
        }

        public async Task UpdateAsync(PlayerInjury playerInjury)
        {
            _db.PlayerInjuries.Update(playerInjury);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlayerInjuryReadModel>> GetReadModelsAsync(
            Guid? playerInjuryId = null,
            Guid? playerId = null,
            Guid? injuryId = null,
            string? status = null,
            string? cause = null,
            bool? isNew = null,
            DateTime? happendBefore = null,
            DateTime? happendAfter = null,
            DateTime? expectedReturnBefore = null,
            DateTime? expectedReturnAfter = null,
            DateTime? returnedBefore = null,
            DateTime? returnedAfter = null,
            int pageNumber = 1,
            int pageSize = 20,
            string? sortColumn = null,
            string? sortDirection = null
            )
        {
            var sql = "dbo.GetPlayerInjuries"; // Name of the stored procedure
           

            var parameters = new {
                 PlayerInjuryId = playerInjuryId,
                 PlayerId = playerId,
                 InjuryId = injuryId,
                 Status = status,
                 Cause = cause,
                 IsNew = isNew,
                 HappendBefore = happendBefore,
                 HappendAfter = happendAfter,
                 ExpectedReturnBefore = expectedReturnBefore,
                 ExpectedReturnAfter = expectedReturnAfter,
                 ReturnedBefore = returnedBefore,
                 ReturnedAfter = returnedAfter,
                 PageNumber = pageNumber,
                 PageSize = pageSize,
                 SortColumn = sortColumn,
                 SortDirection = sortDirection
            };
            using var conn = _dbSettings.CreateReadingConnection();


            var result = await conn.QueryAsync<PlayerInjuryReadModel>(
                sql,
                parameters,
                commandType: System.Data.CommandType.StoredProcedure);

            return result;
        }

        public async Task<bool> ExistesAsync(Guid? playerInjuryId = null, Guid? playerId = null, Guid? injuryId = null)
        {
            return await _db.PlayerInjuries.AnyAsync(
                pi => (!playerInjuryId.HasValue || pi.Id == playerInjuryId.Value) &&
                      (!playerId.HasValue || pi.PlayerId == playerId.Value) &&
                      (!injuryId.HasValue || pi.InjuryId == injuryId.Value));
        }
    }
}
