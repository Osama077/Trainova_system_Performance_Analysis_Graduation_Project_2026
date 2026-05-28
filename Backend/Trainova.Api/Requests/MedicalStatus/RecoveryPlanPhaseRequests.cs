using Trainova.Application.MedicalStatus.PlanPhases.Commands.CreatePlanPhases;
using Trainova.Application.MedicalStatus.PlanPhases.Commands.ResortplanPhases;
using Trainova.Application.MedicalStatus.PlanPhases.Commands.UpdatePlanPhase;

namespace Trainova.Api.Requests.MedicalStatus
{
    public class RecoveryPlanPhaseCreateRequest
    {
        public Guid PlayerInjuryId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int DurationInDays { get; set; }
        public List<string> Activities { get; set; } = new List<string>();
        public CreateRecoveryPlanPhaseCommand ToCommand()
        {
            return new CreateRecoveryPlanPhaseCommand(PlayerInjuryId, Name, DurationInDays, Description, Activities);
        }
    }
    public class RecoveryPlanPhaseResortRequest
    {
        public List<int> NewOrders { get; set; }

        public ResortPlanPhasesCommand ToCommand(Guid playerInjuryId)
        {
            return new ResortPlanPhasesCommand(playerInjuryId, NewOrders);
        }
    }
    public class RecoveryPlanPhaseUpdateRequest
    {
        public string Name { get; set; } = null;
        public string Description { get; set; } = null;
        public int? DurationInDays { get; set; } = null;
        public List<string>? Activities { get; set; } = null;

        public UpdateRecoveryPlanPhaseCommand ToCommand(Guid id)
        {
            return new UpdateRecoveryPlanPhaseCommand(id, Name, Description, DurationInDays, Activities);
        }
    }
}
