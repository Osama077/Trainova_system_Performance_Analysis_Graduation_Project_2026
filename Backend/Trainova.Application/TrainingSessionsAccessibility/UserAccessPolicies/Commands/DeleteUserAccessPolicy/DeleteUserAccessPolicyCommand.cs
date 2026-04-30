using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Commands.DeleteUserAccessPolicy
{
    [Authorize(Role = "HeadCoach,AssistantCoach,SystemAdmin")]
    public record DeleteUserAccessPolicyCommand(Guid Id)
        : IRequest<ResultOf<Done>>;
}
