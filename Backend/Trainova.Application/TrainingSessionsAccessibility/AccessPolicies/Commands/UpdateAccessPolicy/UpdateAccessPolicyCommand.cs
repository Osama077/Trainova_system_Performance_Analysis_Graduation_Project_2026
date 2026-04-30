using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies;

namespace Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Commands.UpdateAccessPolicy
{
    [Authorize(Role = "HeadCoach,AssistantCoach,SystemAdmin")]
    public record UpdateAccessPolicyCommand(
        Guid Id,
        string? PolicyName = null)
        : IRequest<ResultOf<AccessPolicy>>;
}
