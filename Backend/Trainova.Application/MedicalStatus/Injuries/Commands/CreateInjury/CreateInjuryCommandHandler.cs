using MediatR;
using Trainova.Application.Common.Helpers.TimeConverterHelpers;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.MedicalStatus.Injuries.Commands.CreateInjury
{
    public class CreateInjuryCommandHandler(
        IInjuryRepository _injuryrepository,
        IUnitOfWork _unitOfWork,
        CurrentUser _currentUser)
        : IRequestHandler<CreateInjuryCommand, ResultOf<Injury>>
    {

        public async Task<ResultOf<Injury>> Handle(CreateInjuryCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var injury = new Injury(
                    request.Name,
                    request.Description,
                    request.InjuryType,
                    request.TimeAmountInDayes,
                    _currentUser.Id
                    );
                await _unitOfWork.StartTransactionAsync();

                await _injuryrepository.AddAsync(injury);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();
                return injury.AsCreated();
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