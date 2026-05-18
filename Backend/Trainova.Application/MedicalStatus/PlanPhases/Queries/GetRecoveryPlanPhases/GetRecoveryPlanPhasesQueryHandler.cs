using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.MedicalStatus.PlanPhases.Queries.GetRecoveryPlanPhases
{
    public class GetRecoveryPlanPhasesQueryHandler(IRecoveryPlanPhasesRepository _recoveryPlanPhasesRepository) : IRequestHandler<GetRecoveryPlanPhasesQuery, ResultOf<IEnumerable<RecoveryPlanPhase>>>
    {
        public async Task<ResultOf<IEnumerable<RecoveryPlanPhase>>> Handle(GetRecoveryPlanPhasesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var injuries = await _recoveryPlanPhasesRepository.GetByPlayerInjuryIdAsync(request.PlayerInjuryId);

                return injuries.AsDone();
            }
            catch (Exception ex)
            {
                return Error.Unexpected(
                    code: "GetRecoveryPlanPhasesQueryHandler.Handle_Unexpected",
                    description: $"An unexpected error occurred while retrieving injury case Phases.\n {ex.Message}\n");
            }
        }
    }
}
