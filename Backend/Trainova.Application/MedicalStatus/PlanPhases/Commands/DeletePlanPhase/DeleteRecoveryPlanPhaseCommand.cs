using MediatR;
using Trainova.Common.ResultOf;

namespace Trainova.Application.MedicalStatus.PlanPhases.Commands.DeletePlanPhase
{
    public record DeleteRecoveryPlanPhaseCommand(Guid Id) : IRequest<ResultOf<Done>>;
}
