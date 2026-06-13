using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Application.MedicalStatus.PlanPhases.Commands.CreatePlanPhases
{
    public class CreateRecoveryPlanPhaseCommandHandler(
        IPlayerInjuryRepository _playerInjuryRepository,
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<CreateRecoveryPlanPhaseCommand, ResultOf<PlayerInjuryRecoveryPlanData>>
    {
        public async Task<ResultOf<PlayerInjuryRecoveryPlanData>> Handle(CreateRecoveryPlanPhaseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var playerInjury = await _playerInjuryRepository.GetByIdWithPhasesIncludedAsync(request.PlayerInjuryId);

                if (playerInjury is null)
                    return Error.NotFound(
                        code: "CreateRecoveryPlanPhaseCommandHandler.Handle_PlayerInjuryNotFound",
                        description: $"Player injury with id '{request.PlayerInjuryId}' was not found.");

                playerInjury.AddRecoveryPlanPhase(
                    name: request.Name,
                    description: request.Description,
                    durationInDays: request.DurationInDays,
                    activities: request.Activities,
                    insertAtOrder: request.InsertOrder
                    );

                await _unitOfWork.StartTransactionAsync();
                await _playerInjuryRepository.UpdateAsync(playerInjury);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.CommitTransactionAsync();

                return new PlayerInjuryRecoveryPlanData(playerInjury).AsDone();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(
                    code: ex.Code,
                    description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Failure(code: "CreateRecoveryPlanPhaseCommandHandler.Handle_Failure", description: ex.Message);
            }

        }
    }
}
