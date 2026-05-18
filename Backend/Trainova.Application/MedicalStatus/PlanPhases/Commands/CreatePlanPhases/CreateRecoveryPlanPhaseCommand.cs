using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.MedicalStatus.PlanPhases.Commands.CreatePlanPhases
{
    public record CreateRecoveryPlanPhaseCommand(
        Guid PlayerInjuryId,
        string Name,
        DateTime? From,
        DateTime To,
        string? Description = null,
        List<string> Activities = null
        ) : IRequest<ResultOf<RecoveryPlanPhase>>;
}
