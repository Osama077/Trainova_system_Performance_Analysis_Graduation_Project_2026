using Trainova.Domain.Users;

namespace Trainova.Domain.AccessPolicies
{
    public class PolicyUser
    {
        public Guid AccessPoliciesId { get; private set; }
        public AccessPolicy AccessPolicy { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public bool HasAttended { get; private set; }
    }
}
