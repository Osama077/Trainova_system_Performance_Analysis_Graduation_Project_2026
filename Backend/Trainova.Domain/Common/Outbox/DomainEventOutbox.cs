using System.Text.Json;
using Trainova.Domain.Common.BaseEntity;

namespace Trainova.Domain.Common.Outbox
{
    public class DomainEventOutbox : IHasId<Guid>, ICreatorLogable
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string EventType { get; private set; }
        public bool IsHandled { get; private set; } = false;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? HandledAt { get; private set; } = null;
        public List<string> ErrorMessage { get; private set; } = new List<string>();
        public int RetryCount { get; private set; } = 0;
        public string Notification { get; private set; }
        public Guid? CreatedBy { get; private set; }

        public DomainEventOutbox(IDomainEvent notification)
        {
            Id = Guid.NewGuid();
            var type = notification.GetType();
            EventType = type.FullName;
            IsHandled = false;
            Notification = JsonSerializer.Serialize(notification, type);
        }
        public void MarkAsHandled()
        {
            IsHandled = true;
            HandledAt = DateTime.UtcNow;
        }
        public void MarkAsFailed(string message)
        {
            ErrorMessage.Add(message);
            RetryCount++;
        }

        public void SetCreator(Guid creatorId)
        {
            CreatedBy = creatorId;
            CreatedAt = DateTime.UtcNow;
        }

        public DomainEventOutbox() { }
    }
}
