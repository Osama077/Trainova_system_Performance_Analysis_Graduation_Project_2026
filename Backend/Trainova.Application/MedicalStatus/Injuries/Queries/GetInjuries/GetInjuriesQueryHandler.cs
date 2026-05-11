using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.MedicalStatus.Injuries.Queries.GetInjuries
{
    public class GetInjuriesQueryHandler(IInjuryRepository _injuryRepository) : IRequestHandler<GetInjuriesQuery, ResultOf<IEnumerable<Injury>>>
    {
        public async Task<ResultOf<IEnumerable<Injury>>> Handle(GetInjuriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var injuries = await _injuryRepository.GetInjuriesAsync(injuryType: request.InjuryType,SearchTerm: request.SearchTerm);

                return injuries.AsDone();
            }
            catch (Exception ex)
            {
                return Error.Unexpected(
                    code: "GetInjuriesQueryHandler.Handle_Unexpected",
                    description: $"An unexpected error occurred while retrieving injuries.\n {ex.Message}\n");
            }

        }
    }

}
