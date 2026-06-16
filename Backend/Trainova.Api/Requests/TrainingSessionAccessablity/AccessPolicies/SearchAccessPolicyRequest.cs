using Trainova.Api.Models;
using Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Queries.SearchAccessPolicies;

namespace Trainova.Api.Requests.TrainingSessionAccessablity.AccessPolicies;

public class SearchAccessPolicyRequest : Paginator
{
    public string SearchTerm { get; set; } = null;

    public SearchAccessPoliciesQuery ToQuery() => new SearchAccessPoliciesQuery(SearchTerm, Page, PageSize);
}
