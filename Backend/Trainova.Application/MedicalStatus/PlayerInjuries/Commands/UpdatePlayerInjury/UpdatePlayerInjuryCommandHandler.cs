using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Commands.UpdatePlayerInjury
{
    public class UpdatePlayerInjuryCommandHandler(
        IPlayerInjuryRepository _playerInjuryRepository,
        IUnitOfWork _unitOfWork)
        : IRequestHandler<UpdatePlayerInjuryCommand, ResultOf<PlayerInjury>>
    {
        public async Task<ResultOf<PlayerInjury>> Handle(UpdatePlayerInjuryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existing = await _playerInjuryRepository.GetByIdAsync(request.Id);

                if (existing == null)
                {
                    return Error.NotFound(code: "UpdatePlayerInjuryCommandHandler.Handle_NotFound", description: "Player injury not found");
                }

                existing.Update(
                    request.HappendAt,
                    request.Cause,
                    request.SevertiyGrade,
                    request.BodyPart,
                    request.Notes,
                    request.IsNew,
                    request.NewStatus,
                    request.ReturnedAt,
                    request.ExpectedReturnDate
                );

                await _unitOfWork.StartTransactionAsync();

                await _playerInjuryRepository.UpdateAsync(existing);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.CommitTransactionAsync();
                
                return existing.AsNoContent();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Unexpected(code: "UpdatePlayerInjuryCommandHandler.Handle_Unexpected", description: ex.Message);
            }
        }
    }
}
