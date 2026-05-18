using System;
using Trainova.Application.Common.Interfaces.MarkUps;

namespace Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Queries.SearchAccessPolicies;

public class AccessPolicySearchItemResponse : ITotalCountIncluded
{
    public Guid Id { get; set; }
    public string? PolicyName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Usage { get; set; }

    // total count for pagination envelope
    public int TotalCount { get; set; }

    int ITotalCountIncluded.TotalCount => TotalCount;
}
