using MediatR;
using Trainova.Application.Common.Helpers.TimeConverterHelpers;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.MedicalStatus.Injuries;

namespace Trainova.Application.MedicalStatus.Injuries.Commands.DeleteInjury
{
    public record DeleteInjuryCommand(Guid Id) : IRequest<ResultOf<Done>>;
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
                var playerInjuries = await _playerInjuryRepository.GetAllAsync(injuryId:request.Id);

                await _unitOfWork.StartTransactionAsync();

                await _playerInjuryRepository.DeleteRangeAsync(playerInjuries);

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
