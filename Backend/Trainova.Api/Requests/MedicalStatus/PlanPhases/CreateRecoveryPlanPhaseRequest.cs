using Trainova.Application.MedicalStatus.PlanPhases.Commands.CreatePlanPhases;

namespace Trainova.Api.Requests.MedicalStatus.PlanPhases
{
    public class CreateRecoveryPlanPhaseRequest
    {
        public Guid PlayerInjuryId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? From { get; set; } = null;
        public DateTime To { get; set; }
        public List<string> Activities { get; set; } = new List<string>();
        public CreateRecoveryPlanPhaseCommand ToCommand()
        {
            return new CreateRecoveryPlanPhaseCommand(PlayerInjuryId, Name, From, To, Description, Activities);
        }
    }
}
