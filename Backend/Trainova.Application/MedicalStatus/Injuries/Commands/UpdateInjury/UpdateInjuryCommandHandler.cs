using MediatR;
using Trainova.Application.Common.Helpers.TimeConverterHelpers;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.MedicalStatus.Injuries;

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


                TimeConverterHelper timeHelper = null;
                if (request.TimeAmount.HasValue && !string.IsNullOrEmpty(request.TimeType))
                {
                    timeHelper = new TimeConverterHelper
                    {
                        Amount = request.TimeAmount.Value,
                        TimeType = request.TimeType
                    };
                }
                InjuryType? injuryType = null;
                if(request.InjuryType != null)
                {
                    injuryType = Enum.Parse<InjuryType>(request.InjuryType, true);
                }


                injury.Update(
                    name: request.Name,
                    description: request.Description,
                    injuryType: injuryType,
                    averageRecoveryTime: timeHelper.ToTimeSpan());

                await _injuryrepository.UpdateAsync(injury);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

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
