using MediatR;
using Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Common;
using Trainova.Common.ResultOf;

namespace Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Queries.SearchAccessPolicies;

public record SearchAccessPoliciesQuery(
    string? SearchTerm = null,
    int PageNumber = 0,
    int PageSize = 12
    ) : IRequest<ResultOf<IEnumerable<AccessPolicyReadModel>>>;
