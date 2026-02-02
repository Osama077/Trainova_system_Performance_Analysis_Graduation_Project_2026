namespace Trainova.Domain.AccessPolicies
{
    public class AccessPolicy
    {
        public Guid Id { get; private set; }
        public string? PolicyName { get; private set; } = null;

        public ICollection<PolicyUser> PolicyUsers { get; private set; } = [];
    }

}
