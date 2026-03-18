using Trainova.Domain.Common.AuditLogs;

namespace Trainova.Application.Common.Interfaces.Repositories.CommonRepos
{
    public interface IAuditRepository
    {
        Task<IEnumerable<AuditLog>> GetAuditLogsAsync(
            string entityType,
            string? entityId = null,
            int? page = null,
            int? pageSize = null,
            bool IncludeAdded = false,
            bool IncludeDeleted = false,
            bool IncludeUpdated = false);
    }
}
