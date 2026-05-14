using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Commands.CreateAccessPolicy
{
    [Authorize(Roles = "HeadCoach,AssistantCoach,SystemAdmin")]
    public record CreateAccessPolicyCommand(string PolicyName)
        : IRequest<ResultOf<AccessPolicy>>;
}
