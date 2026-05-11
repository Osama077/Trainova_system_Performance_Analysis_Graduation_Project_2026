using System.Text.Json.Serialization;
using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Domain.Common.BaseEntity
{
    public abstract class AuditableEntity<TId> : Entity<TId>, IAuditable<TId>
    {

        public DateTime? LastUpdate { get; protected set; }
        protected AuditLog _audit;

        object IAuditable.Id => Id!;
        [JsonIgnore]
        AuditLog IAuditable.UpdatedAudit { get => _audit; }
        [JsonIgnore]
        AuditLog IAuditable.AddedAudit { get => _audit; }

        protected AuditableEntity(TId id, Guid? createdBy = null) : base(id, createdBy)
        {
            LastUpdate = null;
            var audit = this.CreateCreationAudit();
            _audit = audit;
        }
        protected AuditableEntity(Guid? createdBy = null) : base(createdBy)
        {
            LastUpdate = null;
            var audit = this.CreateCreationAudit();
            _audit = audit;
        }
        protected AuditableEntity()
            : base()
        {
        }


        protected void MarkUpdatedNow()
        {
            var audit = this.CreateUpdateAudit();
            _audit = audit;
            LastUpdate = DateTime.UtcNow;
        }


    }

}
