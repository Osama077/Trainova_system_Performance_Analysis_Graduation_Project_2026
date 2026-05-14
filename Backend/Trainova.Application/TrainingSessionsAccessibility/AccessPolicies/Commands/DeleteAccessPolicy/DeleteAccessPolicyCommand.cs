using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Commands.DeleteAccessPolicy
{
    [Authorize(Roles = "HeadCoach,AssistantCoach,SystemAdmin")]
    public record DeleteAccessPolicyCommand(Guid Id)
        : IRequest<ResultOf<Done>>;
}
