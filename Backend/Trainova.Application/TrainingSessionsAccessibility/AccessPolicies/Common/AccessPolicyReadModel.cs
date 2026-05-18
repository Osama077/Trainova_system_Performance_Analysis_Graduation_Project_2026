using System;
using Trainova.Application.Common.Interfaces.MarkUps;

namespace Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Common;

public class AccessPolicyReadModel : ITotalCountIncluded
{
    public Guid Id { get; set; }
    public string? PolicyName { get; set; }
    // Usage flags
    public bool UsedInPlans { get; set; }
    public bool UsedInTrainingSessions { get; set; }
    public DateTime CreatedAt { get; set; }

    // TotalCount (set by repository when returning paged results)
    public int TotalCount { get; set; }

    int ITotalCountIncluded.TotalCount => TotalCount;
}
