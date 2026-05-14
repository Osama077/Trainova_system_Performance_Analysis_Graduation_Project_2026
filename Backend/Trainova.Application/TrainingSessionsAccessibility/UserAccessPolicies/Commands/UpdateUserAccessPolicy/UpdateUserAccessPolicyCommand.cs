using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Commands.UpdateUserAccessPolicy
{
    [Authorize(Roles = "HeadCoach,AssistantCoach,SystemAdmin,Doctor")]
    public record UpdateUserAccessPolicyCommand(
        Guid Id,
        AttendanceStatus? Status = null,
        decimal? DoneScore = null)
        : IRequest<ResultOf<UserAccessPolicy>>;
}
