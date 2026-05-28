using MediatR;
using Trainova.Common.ResultOf;

namespace Trainova.Application.MedicalStatus.PlanPhases.Commands.CreatePlanPhases
{
    public record CreateRecoveryPlanPhaseCommand(
        Guid PlayerInjuryId,
        string Name,
        int DurationInDays,
        string? Description = null,
        List<string> Activities = null,
        int? InsertOrder = null
        ) : IRequest<ResultOf<PlayerInjuryRecoveryPlanData>>;
}
