using Trainova.Application.TrainingSessionsAccessibility.Plans.Commands.CreatePlan;
using Trainova.Application.TrainingSessionsAccessibility.Plans.Commands.UpdatePlan;
using Trainova.Domain.Common.Enums;

namespace Trainova.Api.Requests.TrainingSessionAccessablity.Plans
{
    public class CreatePlanRequest
    {
        public string PlanName { get; set; }
        public string PlanGoal { get; set; }
        public Guid? AccessPolicyId { get; set; }
        public PlanState PlanState { get; set; } = PlanState.Active;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Guid> UserIds { get; set; } = new List<Guid>();

        public CreatePlanCommand ToCommand()
        {
            return new CreatePlanCommand(
                PlanName,
                PlanGoal,
                PlanState,
                AccessPolicyId,
                StartDate,
                EndDate,
                UserIds);
        }
    }

    public class UpdatePlanRequest
    {
        public string? PlanName { get; set; }
        public string? PlanGoal { get; set; }
        public PlanState? PlanState { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public UpdatePlanCommand ToCommand(Guid id)
        {
            return new UpdatePlanCommand(
                id,
                PlanName,
                PlanGoal,
                PlanState,
                StartDate,
                EndDate);
        }
    }
}
