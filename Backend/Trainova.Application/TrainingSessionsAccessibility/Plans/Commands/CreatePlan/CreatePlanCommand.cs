using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.TrainingSessionsAccessibility.Plans;

namespace Trainova.Application.TrainingSessionsAccessibility.Plans.Commands.CreatePlan
{
    [Authorize(Role = "HeadCoach,AssistantCoach,SystemAdmin")]
    public record CreatePlanCommand(
        string PlanName,
        string PlanGoal,
        Guid AccessPolicyId,
        DateTime StartDate,
        DateTime? EndDate = null)
        : IRequest<ResultOf<Plan>>;
}
