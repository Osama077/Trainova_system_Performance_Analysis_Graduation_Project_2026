using System.Text.Json.Serialization;
using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Domain.Common.BaseEntity
{
    public abstract class AuditableEntity<TId> : Entity<TId>, IAuditable<TId>
    {

        public DateTime? LastUpdate { get; protected set; }
        protected AuditLog _addedAudit;
        protected AuditLog _updatedAudit;
        object IAuditable.Id => Id!;
        [JsonIgnore]
        public AuditLog UpdatedAudit => _updatedAudit;
        [JsonIgnore]
        public AuditLog AddedAudit => _addedAudit;

        protected AuditableEntity(TId id, Guid? createdBy = null) : base(id, createdBy)
        {
            LastUpdate = null;
            var audit = this.CreateCreationAudit();
            _addedAudit = audit;
        }
        protected AuditableEntity(Guid? createdBy = null) : base(createdBy)
        {
            LastUpdate = null;
            var audit = this.CreateCreationAudit();
            _addedAudit = audit;
        }
        protected AuditableEntity()
            : base()
        {
        }


        protected void MarkUpdatedNow()
        {
            var audit = this.CreateUpdateAudit();
            _updatedAudit = audit;
            LastUpdate = DateTime.UtcNow;
        }


    }

}
