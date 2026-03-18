using System.Text.Json;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Domain.Common.AuditLogs
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

        public DateTime From { get; private set; }
        public DateTime ChangedAt { get; private set; }
        public bool IsRecovered { get; private set; } = false;
        public DateTime? RecoveredAt { get; private set; }
        public Guid? UserId { get; private set; }

        //public Guid? DependencyAuditId { get; }
        //public string? DependencyType { get; }




        public void SetUser(Guid userId)
        {
            UserId = userId;
        }



        private AuditLog() { }

        public AuditLog(string entityName, string entityId, AuditActionType action, string? oldValues)
        {
            Id = Guid.NewGuid();
            EntityName = entityName;
            EntityId = entityId;
            Action = action;
            OldValues = oldValues;
        }
        public AuditLog(IAuditable<Guid> auditable, AuditActionType action)
        {
            Id = Guid.NewGuid();
            EntityName = auditable.GetType().Name;
            EntityId = auditable.Id.ToString("N");
            Action = action;
            OldValues = auditable.Serialize();
            From = auditable.LastUpdate ?? auditable.CreatedAt;
            ChangedAt = DateTime.UtcNow;
            IsRecovered = false;
            RecoveredAt = null;
        }



        public static AuditLog Create(IAuditable auditable, AuditActionType action)
        {
            return new AuditLog
            {
                Id = Guid.NewGuid(),
                EntityName = auditable.GetType().Name,
                EntityId = auditable.Id.ToString(),
                Action = action,
                OldValues = JsonSerializer.Serialize(auditable),
                From = auditable.LastUpdate ?? auditable.CreatedAt,
                ChangedAt = DateTime.UtcNow,
                IsRecovered = false,
                RecoveredAt = null
            };
        }





    }

}
