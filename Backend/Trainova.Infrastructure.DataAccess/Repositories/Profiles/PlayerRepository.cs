using Dapper;
using System.Data;
using Trainova.Application.Common.Helpers;
using Trainova.Application.Common.Interfaces.Repositories.Profiles.Players;
using Trainova.Application.Profiles.Players;
using Trainova.Domain.Profiles.Players;
using Trainova.Infrastructure.DataAccess.DbSettingsObjects;

namespace Trainova.Infrastructure.DataAccess.Repositories.Profiles
{
    internal class PlayerRepository : IPlayerRepository
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

        public async Task<IEnumerable<PlayerDetailResponse>> GetPlayersAsync(
            Guid? playerId = null,
            string searchTerm = null,
            Guid? teamId = null,
            int? performanceLevel = null,
            bool? isActive = null,
            int? mainPositionFilter = null,
            int? otherPositionFilter = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            int? minMatches = null,
            string medicalStatus = null,
            int pageNumber = 0,
            int pageSize = 12,
            string sortColumn = "CreatedAt",
            string sortDirection = "DESC")
        {

            var parameters = new
            {
                PlayerId = playerId,
                SearchTerm = searchTerm,
                TeamId = teamId,
                PerformanceLevel = performanceLevel,
                IsActive = isActive,
                MainPositionFilter = mainPositionFilter,
                OtherPositionFilter = otherPositionFilter,
                DateFrom = dateFrom,
                DateTo = dateTo,
                MinMatches = minMatches,
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

        public Task UpdateAsync(Player player)
        {
            _dbContext.Update(player);
            return Task.CompletedTask;
        }
    }
}
