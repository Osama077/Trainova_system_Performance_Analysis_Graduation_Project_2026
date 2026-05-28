using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Application.MedicalStatus.PlanPhases.Commands.ResortplanPhases
{
    public class ResortPlanPhasesCommandHandler(
        IPlayerInjuryRepository _playerInjuryRepository,
        IUnitOfWork _unitOfWork)
        : IRequestHandler<ResortPlanPhasesCommand, ResultOf<PlayerInjuryRecoveryPlanData>>
    {
        public async Task<ResultOf<PlayerInjuryRecoveryPlanData>> Handle(ResortPlanPhasesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var playerInjury = await _playerInjuryRepository.GetByIdWithPhasesIncludedAsync(request.PlayerInjuryId);

                if (playerInjury is null)
                    return Error.NotFound(
                        code: "ResortPlanPhasesCommandHandler.Handle_PlayerInjuryNotFound",
                        description: $"Player injury with id '{request.PlayerInjuryId}' was not found.");


                playerInjury.ReorderPhases(request.NewOrders);


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
                return Error.Failure(code: "ResortPlanPhasesCommandHandler.Handle_Failure", description: ex.Message);
            }

        }
    }

}
