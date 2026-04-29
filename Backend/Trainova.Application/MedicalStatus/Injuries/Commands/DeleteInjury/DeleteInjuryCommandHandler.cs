using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Application.MedicalStatus.Injuries.Commands.DeleteInjury
{
    public class DeleteInjuryCommandHandler(
        IInjuryRepository _injuryRepository,
        IUnitOfWork _unitOfWork,
        IPlayerInjuryRepository _playerInjuryRepository)
        : IRequestHandler<DeleteInjuryCommand, ResultOf<Done>>
    {

        public async Task<ResultOf<Done>> Handle(DeleteInjuryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var injury = await _injuryRepository.GetByIdAsync(request.Id);
                if (injury == null)
                {
                    return Error.NotFound(
                        code: "DeleteInjuryCommandHandler.Handle_NotFound",
                        description: $"Injury with Id {request.Id} not found.");
                }
                if(await _playerInjuryRepository.ExistesAsync(injuryId:request.Id))
                    return Error.Conflict(
                        code: "DeleteInjuryCommandHandler.Handle_Conflict",
                        description: $"Cannot delete Injury with Id {request.Id} because it is associated with existing PlayerInjury records.");

                await _unitOfWork.StartTransactionAsync();


                await _injuryRepository.DeleteAsync(injury);

                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();

                return Done.done.AsNoContent();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(
                    code: ex.Code,
                    description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Failure(description: ex.Message);
            }

        }
    }
}
