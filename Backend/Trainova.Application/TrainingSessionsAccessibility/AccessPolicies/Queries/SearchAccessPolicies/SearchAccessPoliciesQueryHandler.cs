using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Common;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Queries.SearchAccessPolicies;

public class SearchAccessPoliciesQueryHandler(
    IAccessPolicyRepository accessPolicyRepository)
    : IRequestHandler<SearchAccessPoliciesQuery, ResultOf<IEnumerable<AccessPolicyReadModel>>>
{
    public async Task<ResultOf<IEnumerable<AccessPolicyReadModel>>> Handle(SearchAccessPoliciesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var searchTerm = string.IsNullOrWhiteSpace(request.SearchTerm) ? null : request.SearchTerm?.Trim();


            var policies = await accessPolicyRepository.SearchWithUsageAsync(
                searchTerm: searchTerm,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize);


            if (!policies.Any())
                return policies.AsZeroCount();

            return policies.AsPartial();
        }
        catch (Exception ex)
        {
            return Error.Failure(
                code: "SearchAccessPoliciesQueryHandler.Handle_Failure",
                description: ex.Message);
        }
    }
}
