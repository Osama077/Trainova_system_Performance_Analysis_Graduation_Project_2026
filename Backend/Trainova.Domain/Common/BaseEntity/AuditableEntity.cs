using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Domain.Common.BaseEntity
{
    public abstract class AuditableEntity<TId> : Entity<TId>, IAuditable<TId>
    {

        public DateTime? LastUpdate { get; protected set; }
        protected AuditLog _audit;

        object IAuditable.Id => Id!;

        AuditLog IAuditable.UpdatedAudit
        {
            get {
                if (_audit is null)
                    throw new DomainException(
                        code: "AuditNullReference",
                        message: $"Audit hasn't Created or has been disposed");
                if (_audit.Action == AuditActionType.Update)
                    throw new DomainException(
                        code: "AuditTypeMissMatch",
                        message:$"cant get update Audit from Audit with state {_audit.Action.ToString()}");
                return _audit;

                }
        }
        AuditLog IAuditable.AddedAudit
        {
            get {
                if (_audit is null)
                    throw new DomainException(
                        code: "AuditNullReference",
                        message:$"Audit hasn't Created or has been disposed");
                if (_audit.Action == AuditActionType.Create)
                    throw new DomainException(
                        code: "AuditTypeMissMatch",
                        message:$"cant get Create Audit from Audit with state {_audit.Action.ToString()}");
                return _audit;

                }
        }

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
