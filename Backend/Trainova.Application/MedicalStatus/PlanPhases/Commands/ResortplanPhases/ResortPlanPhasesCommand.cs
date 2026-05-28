using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.MedicalStatus.PlanPhases.Commands.ResortplanPhases
{
    public record ResortPlanPhasesCommand(Guid PlayerInjuryId, List<int> NewOrders) : IRequest<ResultOf<PlayerInjuryRecoveryPlanData>>;

}
