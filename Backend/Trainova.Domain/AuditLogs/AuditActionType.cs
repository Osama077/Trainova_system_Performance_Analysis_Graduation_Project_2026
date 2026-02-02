using Trainova.Domain.Common;

namespace Trainova.Domain.AuditLogs
{
    [StoreAsString]
    public enum AuditActionType
    {
        Create = 0,
        Update = 1,
        Delete = 2,
    }
}
