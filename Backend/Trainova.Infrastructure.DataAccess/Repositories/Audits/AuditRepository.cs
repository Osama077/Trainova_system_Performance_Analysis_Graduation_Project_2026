using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Application.Common.Interfaces.Repositories.CommonRepos;
using Trainova.Domain.Common.AuditLogs;

namespace Trainova.Infrastructure.DataAccess.Repositories.Audits
{
    internal class AuditRepository : IAuditRepository
    {
        public Task<IEnumerable<AuditLog>> GetAuditLogsAsync(string entityType, string? entityId = null, int? page = null, int? pageSize = null, bool IncludeAdded = false, bool IncludeDeleted = false, bool IncludeUpdated = false)
        {
            throw new NotImplementedException();
        }
    }
}
