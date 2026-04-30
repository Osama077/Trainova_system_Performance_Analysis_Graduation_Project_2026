using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies;

namespace Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Commands.CreateAccessPolicy
{
    [Authorize(Role = "HeadCoach,AssistantCoach,SystemAdmin")]
    public record CreateAccessPolicyCommand(string PolicyName)
        : IRequest<ResultOf<AccessPolicy>>;
}
