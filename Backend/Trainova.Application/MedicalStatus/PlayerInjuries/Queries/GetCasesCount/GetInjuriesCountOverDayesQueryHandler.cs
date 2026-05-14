using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetCasesCount
{
    public class GetInjuriesCountOverDayesQueryHandler(IPlayerInjuryRepository _playerInjuryRepository) : IRequestHandler<GetInjuriesCasesCountOverDayesQuery, ResultOf<CasesCountResponse>>
    {
        public async Task<ResultOf<CasesCountResponse>> Handle(GetInjuriesCasesCountOverDayesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _playerInjuryRepository.GetInjuriesCountOver(request.Days,request.InjuryId);

                if (data is null)
                {
                    return Error.Conflict(
                        code: "GetInjuriesCountOverDayesQueryHandler.Handle_Conflict",
                        description: "wierd thing happened. m3rfsh ay bsra7a\n").AsError<CasesCountResponse>();
                }

                return data.AsDone();
            }
            catch (Exception ex)
            {
                return Error.Unexpected(
                    code: "GetInjuriesCountOverDayesQueryHandler.Handle_Unexpected",
                    description: $"An unexpected error occurred while retrieving injuries.\n {ex.Message}\n");
            }
        }
    }


}
