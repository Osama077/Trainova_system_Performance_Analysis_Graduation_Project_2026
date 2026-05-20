namespace Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Common;

public class AccessPolicyReadModel
{
    public Guid Id { get; set; }
    public string? PolicyName { get; set; }
    public bool IsSession { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdate { get; set; }
    public int AccessPolicyUsersCount { get; set; }
}
