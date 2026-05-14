using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Application.TrainingSessionsAccessibility.Plans.Commands.UpdatePlan
{
    [Authorize(Roles = "HeadCoach,AssistantCoach,SystemAdmin")]
    public record UpdatePlanCommand(
        Guid Id,
        string? PlanName = null,
        string? PlanGoal = null,
        PlanState? PlanState = null,
        DateTime? StartDate = null,
        DateTime? EndDate = null)
        : IRequest<ResultOf<Plan>>;
}
