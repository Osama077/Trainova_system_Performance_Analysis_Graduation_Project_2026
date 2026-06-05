using Trainova.Domain.Common.BaseEntity;

namespace Trainova.Domain.UserAuth
{
    public class Device : AuditableEntity<Guid>
    {
        public string ServiceName { get; private set; }
        public string DeviceIdentifier { get; private set; }
        public string? ServiceRouting { get; private set; }
        public DeviceType UserType { get; private set; }
        public Guid? RelatedToUserId { get; private set; }
        public User? User { get; private set; }
        public DeviceRole DeviceRole { get; private set; }
        private Device() { }
        public Device(string serviceName, string deviceIdentifier, string? serviceRouting = null, Guid? relatedToUserId = null, Guid? createdBy = null)
            : base(Guid.NewGuid(), createdBy)
        {
            ServiceName = serviceName;
            DeviceIdentifier = deviceIdentifier;
            ServiceRouting = serviceRouting;
            RelatedToUserId = relatedToUserId;
        }
    }
}
