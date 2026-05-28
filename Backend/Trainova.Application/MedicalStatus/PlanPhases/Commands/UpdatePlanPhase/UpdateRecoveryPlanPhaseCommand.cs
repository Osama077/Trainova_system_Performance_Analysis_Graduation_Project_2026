using MediatR;
using Trainova.Common.ResultOf;

namespace Trainova.Application.MedicalStatus.PlanPhases.Commands.UpdatePlanPhase
{
    public record UpdateRecoveryPlanPhaseCommand(
        Guid Id,
        string? Name = null,
        string? Description = null,
        int? DurationInDays = null,
        List<string>? Activities = null) : IRequest<ResultOf<PlayerInjuryRecoveryPlanData>>;
}