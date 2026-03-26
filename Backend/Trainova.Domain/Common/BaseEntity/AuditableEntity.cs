using Trainova.Domain.Common.AuditLogs;

namespace Trainova.Domain.Common.BaseEntity
{
    public abstract class AuditableEntity<TId> : Entity<TId>, IAuditable<TId>
    {

        public DateTime? LastUpdate { get; protected set; }


        object IAuditable.Id => Id!;
        protected AuditableEntity(TId id, Guid? createdBy = null) : base(id, createdBy)
        {
            LastUpdate = null;
        }
        protected AuditableEntity(Guid? createdBy = null) : base(createdBy)
        {
            LastUpdate = null;
        }
        protected AuditableEntity()
            : base()
        {
        }


        protected void MarkUpdatedNow()
        {
            LastUpdate = DateTime.UtcNow;
        }
    }

}
