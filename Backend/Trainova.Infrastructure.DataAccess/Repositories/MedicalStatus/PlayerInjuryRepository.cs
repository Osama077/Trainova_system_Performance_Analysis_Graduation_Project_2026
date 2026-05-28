using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Trainova.Application.Common.Helpers;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.MedicalStatus.PlayerInjuries;
using Trainova.Application.MedicalStatus.PlayerInjuries.Queries;
using Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetCasesCount;
using Trainova.Domain.MedicalStatus;
using Trainova.Infrastructure.DataAccess.DbSettingsObjects;

namespace Trainova.Infrastructure.DataAccess.Repositories.MedicalStatus
{
    public class PlayerInjuryRepository : IPlayerInjuryRepository
    {
        private readonly TrainovaWriteDbContext _dbContext;
        private readonly IDbSettings _dbSettings;

        public PlayerInjuryRepository(TrainovaWriteDbContext db, IDbSettings dbSettings)
        {
            _dbContext = db;
            _dbSettings = dbSettings;
        }

        public async Task AddAsync(PlayerInjury playerInjury)
        {
            await _dbContext.PlayerInjuries.AddAsync(playerInjury);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<PlayerInjury> playerInjuries)
        {
            _dbContext.PlayerInjuries.RemoveRange(playerInjuries);
            await _dbContext.SaveChangesAsync();
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
            IQueryable<PlayerInjury> query = _dbContext.PlayerInjuries.AsQueryable();

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
            _dbContext.PlayerInjuries.Update(playerInjury);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlayerInjuryReadModel>> GetReadModelsAsync(
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
            int pageNumber = 0,
            int pageSize = 12,
            string sortColumn = PlayerInjuryCommonOptions.CreatedAtSortOption,
            string sortDirection = GeneralSortHelper.DESCSortOption
            )
        {
            var sql = "InjuriesData.sp_GetPlayerInjuries"; // Name of the stored procedure


            var parameters = new
            {
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
                SortColumn = sortColumn ?? PlayerInjuryCommonOptions.CreatedAtSortOption,
                SortDirection = sortDirection ?? GeneralSortHelper.DESCSortOption
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
            return await _dbContext.PlayerInjuries.AnyAsync(
                pi => (!playerInjuryId.HasValue || pi.Id == playerInjuryId.Value) &&
                      (!playerId.HasValue || pi.PlayerId == playerId.Value) &&
                      (!injuryId.HasValue || pi.InjuryId == injuryId.Value));
        }

        public async Task<CasesCountResponse> GetInjuriesCountOver(int days = 7, Guid? injuryId = null)
        {
            const string sql = "InjuriesData.sp_GetInjuriesCount";
            var parameters = new
            {
                DaysCount = days, // تغيير الاسم ليتطابق مع الـ Procedure
                InjuryId = injuryId
            };
            using var conn = _dbSettings.CreateReadingConnection();

            return await conn.QueryFirstOrDefaultAsync<CasesCountResponse>(
                sql: sql,
                param: parameters,
                commandType: CommandType.StoredProcedure
            );

        }

        public async Task<IEnumerable<PlayerInjuryReadModel>> GetReadAllModelsAsync(Guid? playerId = null, Guid? injuryId = null, string? status = null, string? cause = null, bool? isNew = null, DateTime? happendBefore = null, DateTime? happendAfter = null, DateTime? expectedReturnBefore = null, DateTime? expectedReturnAfter = null, DateTime? returnedBefore = null, DateTime? returnedAfter = null)
        {
            var sql = "InjuriesData.sp_GetPlayerInjuries"; // Name of the stored procedure


            var parameters = new
            {
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
                PageNumber = 0,
                PageSize = int.MaxValue - 5,
                SortColumn = PlayerInjuryCommonOptions.CreatedAtSortOption,
                SortDirection = GeneralSortHelper.ASCSortOption
            };
            using var conn = _dbSettings.CreateReadingConnection();


            var result = await conn.QueryAsync<PlayerInjuryReadModel>(
                sql,
                parameters,
                commandType: System.Data.CommandType.StoredProcedure);

            return result;
        }

        public async Task<PlayerInjury?> GetByIdAsync(Guid playerInjuryId)
        {
            return await _dbContext.PlayerInjuries.FirstOrDefaultAsync(pi => pi.Id == playerInjuryId);
        }

        public async Task<PlayerInjury?> GetByIdWithPhasesIncludedAsync(Guid playerInjuryId)
        {
            return await _dbContext.PlayerInjuries.Include(pi => pi.Phases).FirstOrDefaultAsync(pi => pi.Id == playerInjuryId);
        }

        public async Task<PlayerInjury> GetPlayerInjuryRelatedToPhasesAsync(Guid phaseId)
        {
            return await _dbContext.PlayerInjuries.Include(pi => pi.Phases).FirstOrDefaultAsync(pi => pi.Phases.Any(pp => pp.Id == phaseId));
        }
    }
}
