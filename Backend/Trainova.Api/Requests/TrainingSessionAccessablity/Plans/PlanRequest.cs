using Trainova.Application.TrainingSessionsAccessibility.Plans.Commands.CreatePlan;
using Trainova.Application.TrainingSessionsAccessibility.Plans.Commands.UpdatePlan;

namespace Trainova.Api.Requests.TrainingSessionAccessablity.Plans
{
    public class CreatePlanRequest
    {
        public string PlanName { get; set; }
        public string PlanGoal { get; set; }
        public Guid AccessPolicyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public CreatePlanCommand ToCommand()
        {
            return new CreatePlanCommand(
                PlanName,
                PlanGoal,
                AccessPolicyId,
                StartDate,
                EndDate);
        }
    }

    public class UpdatePlanRequest
    {
        public string? PlanName { get; set; }
        public string? PlanGoal { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public UpdatePlanCommand ToCommand(Guid id)
        {
            return new UpdatePlanCommand(
                id,
                PlanName,
                PlanGoal,
                StartDate,
                EndDate);
        }
    }
}
