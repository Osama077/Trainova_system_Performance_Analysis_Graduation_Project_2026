using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Application.MedicalStatus.Injuries.Queries.GetInjuryDetailes
{
    public class GetInjuryDetailesQueryHandler(IInjuryRepository _injuryRepository) : IRequestHandler<GetInjuryDetailesQuery, ResultOf<InjuryDetailes>>
    {
        public async Task<ResultOf<InjuryDetailes>> Handle(GetInjuryDetailesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var injury = await _injuryRepository.GetInjyryDetailesAsync(request.Id);
                if (injury is null)
                    return Error.NotFound(code: "GetInjuryDetailesQueryHandler.Handle_Notfounde",
                        description: "no injuryies found with the sended Id"
                        );

                return injury.AsDone();
            }
            catch (Exception ex)
            {
                return Error.Unexpected(
                    code: "GetInjuryDetailesQueryHandler.Handle_Unexpected",
                    description: $"An unexpected error occurred while retrieving injuries.\n {ex.Message}\n");
            }

        }
    }
}
