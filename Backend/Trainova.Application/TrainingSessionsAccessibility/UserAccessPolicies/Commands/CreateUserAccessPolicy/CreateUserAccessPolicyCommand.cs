using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Commands.CreateUserAccessPolicy
{
    [Authorize(Roles = "HeadCoach,AssistantCoach,SystemAdmin")]
    public record CreateUserAccessPolicyCommand(
        Guid AccessPolicyId,
        Guid UserId,
        AttendanceStatus InitialStatus = AttendanceStatus.Waiting)
        : IRequest<ResultOf<UserAccessPolicy>>;
}
