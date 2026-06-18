using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Trainova.Application.Common.Helpers;
using Trainova.Application.Common.Interfaces.Repositories;
using Trainova.Application.Common.Interfaces.Repositories.Profiles.Players;
using Trainova.Application.Profiles.Players;
using Trainova.Application.Profiles.Queries.GetSquadHealthProfiles;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.MedicalStatus;
using Trainova.Domain.Profiles;

namespace Trainova.Infrastructure.DataAccess.Repositories.Profiles
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IDbSettings _dbSettings;
        private readonly TrainovaWriteDbContext _dbContext;

        public PlayerRepository(TrainovaWriteDbContext dbContext, IDbSettings dbSettings)
        {
            _dbContext = dbContext;
            _dbSettings = dbSettings;
        }

        public async Task AddAsync(Player player)
        {
            await _dbContext.AddAsync(player);
        }

        public async Task<Player?> GetByIdAsync(Guid playerId)
        {
            return await _dbContext.Players
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == playerId);
        }

        public async Task<IEnumerable<PlayerDetailResponse>> GetPlayersAsync(
            Guid? playerId = null,
            string? searchTerm = null,
            int? performanceLevel = null,
            bool? isActive = null,
            int? mainPositionFilter = null,
            int? otherPositionFilter = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            string? medicalStatus = null,
            int pageNumber = 0,
            int pageSize = 12,
            string sortColumn = "CreatedAt",
            string sortDirection = "DESC")
        {

            var parameters = new
            {
                PlayerId = playerId,
                SearchTerm = searchTerm,
                PerformanceLevel = performanceLevel,
                IsActive = isActive,
                MainPositionFilter = mainPositionFilter,
                OtherPositionFilter = otherPositionFilter,
                DateFrom = dateFrom,
                DateTo = dateTo,
                MedicalStatus = medicalStatus,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortColumn = sortColumn,
                SortDirection = sortDirection == GeneralSortHelper.ASCSortOption
                    ? GeneralSortHelper.ASCSortOption
                    : GeneralSortHelper.DESCSortOption
            };

            var sql = "playersData.sp_GetPlayersFiltered";

            using var conn = _dbSettings.CreateReadingConnection();

            return await conn.QueryAsync<PlayerDetailResponse>(
                sql: sql,
                param: parameters,
                commandType: CommandType.StoredProcedure,
                commandTimeout: 5
                );

        }

        public async Task<IEnumerable<SquadHealthProfilesDataReadingModel>> GetSquadHealthProfiles(Position? position = null, InjuryStatus? injuryStatus = null, SeverityGrade? severityGrade = null, string? searchName = null)
        {
            const string sql = "InjuriesData.sp_GetSquadHealthDashboard";
            var parameters = new
            {
                Position = position,
                InjuryStatus = injuryStatus,
                SeverityGrade = severityGrade,
                SearchName = searchName
            };

            using var connection = _dbSettings.CreateReadingConnection();

            return await connection.QueryAsync<SquadHealthProfilesDataReadingModel>(
                sql,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public Task UpdateAsync(Player player)
        {
            _dbContext.Update(player);
            return Task.CompletedTask;
        }
    }
}
