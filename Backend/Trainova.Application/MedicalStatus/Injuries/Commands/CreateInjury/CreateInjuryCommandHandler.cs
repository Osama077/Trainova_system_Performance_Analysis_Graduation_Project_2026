using MediatR;
using Trainova.Application.Common.Helpers.TimeConverterHelpers;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.MedicalStatus.Injuries;

namespace Trainova.Application.MedicalStatus.Injuries.Commands.CreateInjury
{
    public class CreateInjuryCommandHandler(
        IInjuryRepository _injuryrepository,
        CurrentUser _currentUser)
        : IRequestHandler<CreateInjuryCommand, ResultOf<Injury>>
    {

        public async Task<ResultOf<Injury>> Handle(CreateInjuryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var timeAttacher = new TimeConverterHelper
                {
                    TimeType = request.TimeType,
                    Amount = request.TimeAmount
                };

                var injuryType = Enum.Parse<InjuryType>(request.InjuryType, true);

                var injury = new Injury(
                    request.Name,
                    request.Description,
                    injuryType,
                    timeAttacher.ToTimeSpan()
                    );
                await _injuryrepository.AddAsync(injury);
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