using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.MedicalStatus.PlanPhases.Queries.GetRecoveryPlanPhases
{
    public record GetRecoveryPlanPhasesQuery(
        Guid PlayerInjuryId
        ) : IRequest<ResultOf<IEnumerable<RecoveryPlanPhase>>>;
}
