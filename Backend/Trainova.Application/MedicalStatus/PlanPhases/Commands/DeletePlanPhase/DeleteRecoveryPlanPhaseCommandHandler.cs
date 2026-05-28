using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Application.MedicalStatus.PlanPhases.Commands.DeletePlanPhase
{
    public class DeleteRecoveryPlanPhaseCommandHandler(
        IPlayerInjuryRepository _playerInjuryRepository,
        IUnitOfWork _unitOfWork)
        : IRequestHandler<DeleteRecoveryPlanPhaseCommand, ResultOf<Done>>
    {
        public async Task<ResultOf<Done>> Handle(DeleteRecoveryPlanPhaseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var injuryCase = await _playerInjuryRepository.GetPlayerInjuryRelatedToPhasesAsync(request.Id);
                if (injuryCase == null)
                    return Error.NotFound(
                        code: "DeleteRecoveryPlanPhaseCommandHandler.Handle_PlayerInjuryNotFound",
                        description: $"Player injury Phase with id '{request.Id}' was not found.");

                var phase = injuryCase.Phases.FirstOrDefault(p => p.Id == request.Id);

                var order = phase.Order;
                var id = phase.Id;
                injuryCase.RemovePhase(order);

                await _unitOfWork.StartTransactionAsync();

                await _playerInjuryRepository.UpdateAsync(injuryCase);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();


                return new Done(id: id).AsNoContent();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(
                    code: ex.Code,
                    description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Failure(code: "DeleteRecoveryPlanPhaseCommandHandler.Handle_Failure", description: ex.Message);
            }

        }
    }
}
