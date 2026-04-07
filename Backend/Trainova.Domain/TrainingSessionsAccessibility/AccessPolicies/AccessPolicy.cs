using Trainova.Domain.Common.BaseEntity;

namespace Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies
{
    public class AccessPolicy : AuditableEntity<Guid>
    {
        public string? PolicyName { get; private set; } = null;

        public ICollection<UserAccessPolicy> PolicyUsers { get; private set; } = [];
        private AccessPolicy() : base(){ }
        public AccessPolicy(string? policyName = null, Guid? createdBy = null)
            : base(createdBy)
        {
            PolicyName = policyName;
        }
    }

}
