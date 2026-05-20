using Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Queries.SearchAccessPolicies;

namespace Trainova.Api.Requests.TrainingSessionAccessablity.AccessPolicies;

public class SearchAccessPolicyRequest
{
    public string SearchTerm { get; set; } = null;
    public bool? IsSession { get; set; } = null;
    public int PageNumber { get; set; } = 0;
    public int PageSize { get; set; } = 12;
    public SearchAccessPoliciesQuery ToQuery() => new SearchAccessPoliciesQuery(SearchTerm, IsSession, PageNumber, PageSize);
}
