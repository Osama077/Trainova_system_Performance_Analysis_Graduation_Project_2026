using Trainova.Domain.Common.Helpers;

namespace Trainova.Domain.Common.AuditLogs
{
    [StoreAsString]
    public enum AuditActionType
    {
        Create = 0,
        Update = 1,
        Delete = 2,
    }
}
