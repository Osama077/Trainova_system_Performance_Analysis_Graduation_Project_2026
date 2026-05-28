using MediatR;
using Trainova.Common.ResultOf;

namespace Trainova.Application.MedicalStatus.PlanPhases.Queries.GetRecoveryPlanPhases
{
    public record GetRecoveryPlanPhasesQuery(
        Guid PlayerInjuryId
        ) : IRequest<ResultOf<PlayerInjuryRecoveryPlanData>>;

}
