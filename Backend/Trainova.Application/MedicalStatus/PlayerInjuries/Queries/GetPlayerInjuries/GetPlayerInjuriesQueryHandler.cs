using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetPlayerInjuries
{
    public class GetPlayerInjuriesQueryHandler(
        IPlayerInjuryRepository _playerInjuryRepository,
        CurrentUser _currentUser)
        : IRequestHandler<GetPlayerInjuriesQuery, ResultOf<IEnumerable<PlayerInjuryReadModel>>>
    {

        public async Task<ResultOf<IEnumerable<PlayerInjuryReadModel>>> Handle(GetPlayerInjuriesQuery request, CancellationToken cancellationToken)
        {
            try
            {

                if (_currentUser.Roles.Contains(RolesStaticSeeding.Player) && (request.PlayerId.HasValue || _currentUser.Id != request.PlayerId))
                    return Error.Forbidden(
                        code: "GetPlayerInjuriesQueryHandler.Handle_Forbidden",
                        description: "players cant get any other players injuries"
                        );

                var items = await _playerInjuryRepository.GetReadModelsAsync(
                            request.PlayerInjuryId,
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
                            request.ReturnedAfter,
                            request.Page + 1, // API page is zero-based; SP expects 1-based
                            request.PageSize,
                            request.SortColumn,
                            request.SortDirection
                            );

                if (!items.Any())
                {
                    return items.AsZeroCount();
                }

                return items.AsPartial();
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "GetPlayerInjuriesQueryHandler.Handle_Failure",
                    description:ex.Message
                    );
            }
        }
    }
}
