using MediatR;
using System.Text.Json;
using Trainova.Domain.Common.BaseEntity;

namespace Trainova.Domain.Common.Outbox
{
    public class DomainEventOutbox : IHasId<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string EventType { get; set; }
        public bool IsHandled { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? HandledAt { get; set; } = null;
        public List<string> ErrorMessage { get; set; } = new List<string>();
        public int RetryCount { get; set; } = 0;
        public string Notification { get; set; }
        public DomainEventOutbox(IDomainEvent notification)
        {
            Id = Guid.NewGuid();
            var type = notification.GetType();
            EventType = type.FullName;
            CreatedAt = DateTime.UtcNow;
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
        public DomainEventOutbox() { }
    }
}
