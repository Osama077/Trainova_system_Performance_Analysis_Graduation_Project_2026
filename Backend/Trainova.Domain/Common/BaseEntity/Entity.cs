
namespace Trainova.Domain.Common.BaseEntity
{
    public abstract class Entity<TId> : IEntity<TId>
    {
        public TId Id { get; protected set; } = default!;
        public Guid? CreatedBy { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

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
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        void IEntity<TId>.AddDomainEvent(IDomainEvent domainEvent)
        {
            AddDomainEvent(domainEvent);
        }
    }
    public interface IEntity<TId>: IHasId<TId>,ICreatorLogable
    {
        void AddDomainEvent(IDomainEvent domainEvent);
    }
}
