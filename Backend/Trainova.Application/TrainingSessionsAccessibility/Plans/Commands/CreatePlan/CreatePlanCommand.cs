using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Application.TrainingSessionsAccessibility.Plans.Commands.CreatePlan
{
    [Authorize(Roles = "HeadCoach,AssistantCoach,SystemAdmin")]
    public record CreatePlanCommand(
        string PlanName,
        string PlanGoal,
        PlanState PlanState,
        Guid? AccessPolicyId,
        DateTime StartDate,
        DateTime? EndDate,
        List<Guid> UserIds)
        : IRequest<ResultOf<Plan>>;
}
