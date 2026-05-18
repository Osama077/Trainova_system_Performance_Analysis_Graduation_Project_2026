using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Queries.SearchAccessPolicies;

public class SearchAccessPoliciesQueryHandler(
    IAccessPolicyRepository accessPolicyRepository)
    : IRequestHandler<SearchAccessPoliciesQuery, ResultOf<IEnumerable<AccessPolicySearchItemResponse>>>
{
    public async Task<ResultOf<IEnumerable<AccessPolicySearchItemResponse>>> Handle(SearchAccessPoliciesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Normalize empty strings to null so repository doesn't filter on empty values
            var searchTerm = string.IsNullOrWhiteSpace(request.SearchTerm) ? null : request.SearchTerm?.Trim();
            var usageType = string.IsNullOrWhiteSpace(request.UsageType) ? null : request.UsageType?.Trim();

            // Provide safe defaults for paging when caller omitted values
            var pageNumber = request.PageNumber.HasValue && request.PageNumber.Value >= 0 ? request.PageNumber.Value : 0;
            var pageSize = request.PageSize.HasValue && request.PageSize.Value > 0 ? request.PageSize.Value : 8;

            var policies = (await accessPolicyRepository.SearchWithUsageAsync(
                searchTerm: searchTerm,
                usageType: usageType,
                pageNumber: pageNumber,
                pageSize: pageSize,
                sortColumn: request.SortColumn,
                sortDirection: request.SortDirection)).ToList();

            var listResult = policies.Select(p => new AccessPolicySearchItemResponse
            {
                Id = p.Id,
                PolicyName = p.PolicyName,
                CreatedAt = p.CreatedAt,
                Usage = p.UsedInPlans && p.UsedInTrainingSessions ? "Both" : p.UsedInPlans ? "Plan" : p.UsedInTrainingSessions ? "TrainingSession" : "Unused",
                TotalCount = p.TotalCount
            }).ToList();

            IEnumerable<AccessPolicySearchItemResponse> result = listResult;

            if (!result.Any())
                return result.AsZeroCount();

            return result.AsPartial();
        }
        catch (Exception ex)
        {
            return Error.Failure(
                code: "SearchAccessPoliciesQueryHandler.Handle_Failure",
                description: ex.Message);
        }
    }
}
