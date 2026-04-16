using MediatR;

namespace Trainova.Domain.Common.BaseEntity
{
    public abstract class Entity<TId> : ILogableCreator
    {
        public TId Id { get; protected set; } = default!;
        public Guid? CreatedBy { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        private List<INotification> _domainEvents = new List<INotification>();

        protected Entity(Guid? createdBy = null)
        {
            CreatedBy = createdBy;
            CreatedAt = DateTime.Now;
        }
        protected Entity(TId id, Guid? createdBy = null)
        {
            Id = id;
            CreatedBy = createdBy;
            CreatedAt = DateTime.Now;
        }
        protected Entity()
        {
        }
        public void SetCreator(Guid creatorId)
        {
            if (CreatedBy is not null)
                return;

            CreatedBy = creatorId;
        }
        protected void AddDomainEvent(INotification domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }

}
