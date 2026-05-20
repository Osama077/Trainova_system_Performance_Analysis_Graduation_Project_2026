using Trainova.Domain.Common.BaseEntity;

namespace Trainova.Domain.TrainingSessionsAccessibility
{
    public class AccessPolicy : AuditableEntity<Guid>
    {
        public string PolicyName { get; private set; } = null;
        public bool IsSession {  get; private set; } = false;

        public ICollection<UserAccessPolicy> PolicyUsers { get; private set; } = [];
        private AccessPolicy() : base() {}
        // ...existing code...
        public AccessPolicy(string policyName, bool isSession = false, Guid? createdBy = null)
            : base(Guid.NewGuid(), createdBy)
        {
            IsSession = isSession;
            PolicyName = policyName;
        }

        public void Update(string? policyName = null)
        {
            if (!string.IsNullOrWhiteSpace(policyName))
                PolicyName = policyName;
        }

        public AccessPolicy CopyAccessPolicy(out List<UserAccessPolicy> userAccessPolicies,string? newName= null)
        {
            userAccessPolicies = PolicyUsers.Select(up => new UserAccessPolicy(Id,up.UserId, AttendanceStatus.Waiting)).ToList();
            return new AccessPolicy(newName?? PolicyName);
        }
    }

}
