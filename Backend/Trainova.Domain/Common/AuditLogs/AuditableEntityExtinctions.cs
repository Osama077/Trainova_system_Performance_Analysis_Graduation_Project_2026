using System.Security;

namespace Trainova.Domain.Common.AuditLogs
{
    public static class AuditableEntityExtinctions
    {
        public static AuditLog CreateUpdateAudit(this IAuditable auditable)
        {
            return AuditLog.Create(auditable, AuditActionType.Update);
        }
        public static AuditLog CreateCreationAudit(this IAuditable auditable)
        {
            return AuditLog.Create(auditable, AuditActionType.Create);
        }
        public static AuditLog CreateDeletionAudit(this IAuditable auditable)
        {
            return AuditLog.Create(auditable, AuditActionType.Delete);
        }
    }
}
