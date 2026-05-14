using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Commands.DeleteUserAccessPolicy
{
    [Authorize(Roles = "HeadCoach,AssistantCoach,SystemAdmin")]
    public record DeleteUserAccessPolicyCommand(Guid Id)
        : IRequest<ResultOf<Done>>;
}
