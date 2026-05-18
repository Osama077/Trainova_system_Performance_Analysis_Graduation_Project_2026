using Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Queries.SearchAccessPolicies;

namespace Trainova.Api.Requests.TrainingSessionAccessablity.AccessPolicies;

public record SearchAccessPolicyRequest(
    string? SearchTerm,
    string? UsageType = null,
    int? PageNumber = null,
    int? PageSize = null,
    string SortColumn = "CreatedAt",
    string SortDirection = "DESC")
{
    public SearchAccessPoliciesQuery ToQuery() => new SearchAccessPoliciesQuery(SearchTerm, UsageType, PageNumber, PageSize, SortColumn, SortDirection);
}
