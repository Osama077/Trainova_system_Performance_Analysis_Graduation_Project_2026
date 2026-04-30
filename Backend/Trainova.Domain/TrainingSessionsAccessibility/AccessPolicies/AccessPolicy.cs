using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.TrainingSessionsAccessibility.Plans;
using Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions;

namespace Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies
{
    public class AccessPolicy : AuditableEntity<Guid>
    {
        public string? PolicyName { get; private set; } = null;

        public ICollection<UserAccessPolicy> PolicyUsers { get; private set; } = [];
        private AccessPolicy() : base()
        {}
        // ...existing code...
        public AccessPolicy(string? policyName = null, Guid? createdBy = null)
            : base(Guid.NewGuid(), createdBy)
        {
            PolicyName = policyName;
        }

        public void Update(string? policyName = null)
        {
            if (!string.IsNullOrWhiteSpace(policyName))
                PolicyName = policyName;
        }
    }

}
