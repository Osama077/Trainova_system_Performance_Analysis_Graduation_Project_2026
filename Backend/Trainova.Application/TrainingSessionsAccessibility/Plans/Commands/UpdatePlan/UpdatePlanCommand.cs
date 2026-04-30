using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.TrainingSessionsAccessibility.Plans;

namespace Trainova.Application.TrainingSessionsAccessibility.Plans.Commands.UpdatePlan
{
    [Authorize(Role = "HeadCoach,AssistantCoach,SystemAdmin")]
    public record UpdatePlanCommand(
        Guid Id,
        string? PlanName = null,
        string? PlanGoal = null,
        DateTime? StartDate = null,
        DateTime? EndDate = null)
        : IRequest<ResultOf<Plan>>;
}
