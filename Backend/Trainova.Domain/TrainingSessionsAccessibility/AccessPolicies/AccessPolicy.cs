using Trainova.Domain.Common.AuditLogs;

namespace Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies
{
    public class AccessPolicy : IAuditable<Guid>
    {
        public Guid Id { get; private set; }
        public string? PolicyName { get; private set; } = null;

        public DateTime CreatedAt { get; private set; } = DateTime.Now;

        public DateTime? LastUpdate { get; private set; } = null;

        public Guid? CreatedBy { get; private set; }
        object IAuditable.Id => Id;

        public ICollection<PolicyUser> PolicyUsers { get; private set; } = [];

        public AccessPolicy(string? policyName = null)
        {
            Id = Guid.NewGuid();
            PolicyName = policyName;
        }
    }

}
