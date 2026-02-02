using Trainova.Domain.Common;

namespace Trainova.Domain.AuditLogs
{
    public class AuditLog
    {
        // Primary Key
        public Guid Id { get; private set; }
        // entity being audited
        public string EntityName { get; private set; } = default!;
        // ID of the entity being audited (as string for flexibility) guid.tostring() , int.tostring() etc
        public string EntityId { get; private set; } = default!;
        // action performed (create, update, delete)
        public AuditActionType Action { get; private set; } = default!;
        // old values before the action (as JSON string)
        public string? OldValues { get; private set; }

        public DateTime From { get; }
        public DateTime ChangedAt { get; }
        public bool IsRecovered { get; private set; } = false;
        public DateTime? RecoveredAt { get; private set; }
        public Guid? UserId { get; private set; }

        //public Guid? DependencyAuditId { get; }
        //public string? DependencyType { get; }




        public void SetUser(Guid userId)
        {
            UserId = userId;
        }
        public void SetDependency(Guid dependencyAuditId, string dependencyType)
        {
            throw new NotImplementedException();
        }


        private AuditLog() { }

        public AuditLog(string entityName, string entityId, int action, string? oldValues)
        {
            Id = Guid.NewGuid();
            EntityName = entityName;
            EntityId = entityId;
            Action = (AuditActionType)action;
            OldValues = oldValues;
        }
        public AuditLog(IAuditable auditable, AuditActionType action, string entityId)
        {
            Id = Guid.NewGuid();
            EntityName = auditable.GetType().Name;
            EntityId = entityId;
            Action = action;
            OldValues = auditable.Serialize();
            From = auditable.LastUpdate??auditable.CreatedAt;
            ChangedAt = DateTime.UtcNow;
            IsRecovered = false;
            RecoveredAt = null;
        }
    }

}
