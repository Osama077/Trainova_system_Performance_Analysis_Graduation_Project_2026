using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Application.MedicalStatus.PlanPhases.Commands.UpdatePlanPhase
{
    public class UpdateRecoveryPlanPhaseCommandHandler(
        IPlayerInjuryRepository _playerInjuryRepository,
        IUnitOfWork _unitOfWork)
        : IRequestHandler<UpdateRecoveryPlanPhaseCommand, ResultOf<PlayerInjuryRecoveryPlanData>>
    {
        public async Task<ResultOf<PlayerInjuryRecoveryPlanData>> Handle(UpdateRecoveryPlanPhaseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var injuryCase = await _playerInjuryRepository.GetPlayerInjuryRelatedToPhasesAsync(request.Id);
                if (injuryCase == null)
                    return Error.NotFound(
                        code: "UpdateRecoveryPlanPhaseCommandHandler.Handle_PlayerInjuryNotFound",
                        description: $"Player injury with related phase id '{request.Id}' was not found.");

                injuryCase.UpdateRecoveryPlanPhase(
                    phaseId: request.Id,
                    name: request.Name,
                    description: request.Description,
                    durationInDays: request.DurationInDays,
                    activities: request.Activities
                );

                await _unitOfWork.StartTransactionAsync();

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.CommitTransactionAsync();

                return new PlayerInjuryRecoveryPlanData(injuryCase).AsDone();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(
                    code: ex.Code,
                    description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "UpdateRecoveryPlanPhaseCommandHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }
}