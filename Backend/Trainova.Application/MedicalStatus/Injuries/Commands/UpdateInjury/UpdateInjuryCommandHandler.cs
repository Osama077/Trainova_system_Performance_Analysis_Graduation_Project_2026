using MediatR;
using Trainova.Application.Common.Helpers.TimeConverterHelpers;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.MedicalStatus.Injuries.Commands.UpdateInjury
{
    public class UpdateInjuryCommandHandler(
        IInjuryRepository _injuryrepository,
        IUnitOfWork _unitOfWork)
        : IRequestHandler<UpdateInjuryCommand, ResultOf<Injury>>
    {

        public async Task<ResultOf<Injury>> Handle(UpdateInjuryCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var injury = await _injuryrepository.GetByIdAsync(request.Id);

                if(injury == null)
                {
                    return Error.NotFound(
                        code: "UpdateInjuryCommandHandler.Handle_NotFound",
                        description: $"Injury with Id {request.Id} not found.");
                }

                await _unitOfWork.StartTransactionAsync();

                injury.Update(
                    name: request.Name,
                    description: request.Description,
                    injuryType: request.InjuryType,
                    averageRecoveryTime: request.TimeAmountInDayes);

                await _injuryrepository.UpdateAsync(injury);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.CommitTransactionAsync();

                return injury.AsDone();
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
