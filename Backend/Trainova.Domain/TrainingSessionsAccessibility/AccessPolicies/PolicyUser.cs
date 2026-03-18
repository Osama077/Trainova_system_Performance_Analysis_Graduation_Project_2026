using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.UserAuth.Users;

namespace Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies
{
    public class PolicyUser :IAuditable<Guid>
    {
        public Guid Id { get; private set; }

        public Guid AccessPoliciesId { get; private set; }
        public AccessPolicy AccessPolicy { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public bool HasAttended { get; private set; }


        public DateTime CreatedAt { get; private set; }

        public DateTime? LastUpdate { get; private set; }

        public Guid? CreatedBy { get; private set; }
        object IAuditable.Id => Id;

    }
    public enum AttendanceStatus
    {
        Cancelled = 1,
        Done,
        Skipped
    }
}
