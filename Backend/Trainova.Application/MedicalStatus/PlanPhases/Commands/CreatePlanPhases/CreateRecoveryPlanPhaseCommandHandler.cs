using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.MedicalStatus.PlanPhases.Commands.CreatePlanPhases
{
    public class CreateRecoveryPlanPhaseCommandHandler(
        IRecoveryPlanPhasesRepository _planPhasesRepository,
        IPlayerInjuryRepository _playerInjuryRepository,
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<CreateRecoveryPlanPhaseCommand, ResultOf<RecoveryPlanPhase>>
    {
        public async Task<ResultOf<RecoveryPlanPhase>> Handle(CreateRecoveryPlanPhaseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var playerInjury = await _playerInjuryRepository.GetByIdWithPhasesIncludedAsync(request.PlayerInjuryId);

                if (playerInjury is null)
                    return Error.NotFound(
                        code: "CreateRecoveryPlanPhaseCommandHandler.Handle_PlayerInjuryNotFound",
                        description: $"Player injury with id '{request.PlayerInjuryId}' was not found.");


                var phase = new RecoveryPlanPhase(
                    playerInjuryId: playerInjury.Id,
                    name: request.Name,
                    description: request.Description,
                    to: request.To,
                    from: request.From,
                    activties: request.Activities);



                await _unitOfWork.StartTransactionAsync();

                playerInjury.AddRecoveryPlanPhase(phase);

                await _planPhasesRepository.AddAsync(phase);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.CommitTransactionAsync();

                return phase;
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
