using MediatR;
using Trainova.Common.ResultOf;

namespace Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Queries.SearchAccessPolicies;

public record SearchAccessPoliciesQuery : IRequest<ResultOf<IEnumerable<AccessPolicySearchItemResponse>>>
{
    public string? SearchTerm { get; init; }
    public string? UsageType { get; init; }
    public int? PageNumber { get; init; } = null;
    public int? PageSize { get; init; } = null;
    public string SortColumn { get; init; } = "CreatedAt";
    public string SortDirection { get; init; } = "DESC";

    public SearchAccessPoliciesQuery(string? searchTerm = null, string? usageType = null, int? pageNumber = null, int? pageSize = null, string sortColumn = "CreatedAt", string sortDirection = "DESC")
    {
        SearchTerm = searchTerm;
        UsageType = usageType;
        PageNumber = pageNumber;
        PageSize = pageSize;
        SortColumn = sortColumn;
        SortDirection = sortDirection;
    }
}
