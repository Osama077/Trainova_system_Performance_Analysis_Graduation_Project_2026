using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Application.MedicalStatus.PlanPhases.Queries.GetRecoveryPlanPhases
{
    public class GetRecoveryPlanPhasesQueryHandler(IPlayerInjuryRepository _playerInjuryRepository) : IRequestHandler<GetRecoveryPlanPhasesQuery, ResultOf<PlayerInjuryRecoveryPlanData>>
    {
        public async Task<ResultOf<PlayerInjuryRecoveryPlanData>> Handle(GetRecoveryPlanPhasesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var injuryCase = await _playerInjuryRepository.GetByIdWithPhasesIncludedAsync(request.PlayerInjuryId);

                if (injuryCase == null)
                {
                    return Error.NotFound(
                        code: "GetRecoveryPlanPhasesQueryHandler.Handle_NotFound",
                        description: $"No injury case found with Id: {request.PlayerInjuryId}");
                }


                return new PlayerInjuryRecoveryPlanData(injuryCase).AsDone();
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
