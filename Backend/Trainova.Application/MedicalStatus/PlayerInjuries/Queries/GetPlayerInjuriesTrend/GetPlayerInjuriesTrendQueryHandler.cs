using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetPlayerInjuriesTrend
{
    public class GetPlayerInjuriesTrendQueryHandler(
        IPlayerInjuryRepository _playerInjuryRepository)
        : IRequestHandler<GetPlayerInjuriesTrendQuery, ResultOf<PlayerInjuriesTrendResult>>
    {
        public async Task<ResultOf<PlayerInjuriesTrendResult>> Handle(GetPlayerInjuriesTrendQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var items = await _playerInjuryRepository.GetReadAllModelsAsync(
                            request.PlayerId,
                            request.InjuryId,
                            request.Status,
                            request.Cause,
                            request.IsNew,
                            request.HappendBefore,
                            request.HappendAfter,
                            request.ExpectedReturnBefore,
                            request.ExpectedReturnAfter,
                            request.ReturnedBefore,
                            request.ReturnedAfter
                            );

                if (!items.Any())
                {
                    return new PlayerInjuriesTrendResult(items).AsZeroCount();
                }

                return new PlayerInjuriesTrendResult(items).AsPartial();
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "GetPlayerInjuriesTrendQueryHandler.Handle_Failure",
                    description: ex.Message
                    );
            }
        }
    }
}
