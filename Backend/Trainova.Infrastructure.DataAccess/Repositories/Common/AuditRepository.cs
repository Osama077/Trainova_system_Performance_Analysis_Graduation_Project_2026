using Dapper;
using Microsoft.EntityFrameworkCore;
using Trainova.Application.Common.Interfaces.Repositories.CommonRepos;
using Trainova.Domain.Common.AuditLogs;
using Trainova.Infrastructure.DataAccess.DbSettingsObjects;

namespace Trainova.Infrastructure.DataAccess.Repositories.Common
{
    public class AuditRepository : IAuditRepository
    {
        private readonly IDbSettings _dbSettings;
        public readonly TrainovaWriteDbContext _context;

        public AuditRepository(IDbSettings dbSettings, TrainovaWriteDbContext context)
        {
            _dbSettings = dbSettings;
            _context = context;
        }




        // Obsolete
        /*

        public Task<IEnumerable<AuditLog>> GetAuditLogsAsync(string entityType, string? entityId = null, int? page = null, int? pageSize = null, bool IncludeAdded = false, bool IncludeDeleted = false, bool IncludeUpdated = false)
        {
            const string query = "";
            var parameters = new
            {
                EntityType = entityType,
                EntityId = entityId,
                Page = page,
                PageSize = pageSize,
                IncludeAdded = IncludeAdded,
                IncludeDeleted = IncludeDeleted,
                IncludeUpdated = IncludeUpdated
            };

            using var connection = _dbSettings.CreateReadingConnection();

            return connection.QueryAsync<AuditLog>(
                sql: query,
                param: parameters,
                commandType: System.Data.CommandType.StoredProcedure
                );
        }
        */
        public async Task<IEnumerable<AuditLog>> GetAuditLogsAsync(string entityType, string? entityId = null, int? page = null, int? pageSize = null, bool IncludeAdded = false, bool IncludeDeleted = false, bool IncludeUpdated = false)
        {
            return await _context.AuditLoges
                .Where(log => log.EntityName == entityType
                    && (entityId == null || log.EntityId == entityId)
                    && ((IncludeAdded && log.Action == AuditActionType.Create)
                        || (IncludeDeleted && log.Action == AuditActionType.Delete)
                        || (IncludeUpdated && log.Action == AuditActionType.Update)))
                .Skip((page ?? 0) * (pageSize ?? 10))
                .Take(pageSize ?? 10)
                .ToListAsync();
        }
    }
}
