using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Common.ResultOf;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetPlayerInjuries
{
    public class GetPlayerInjuriesQueryHandler : IRequestHandler<GetPlayerInjuriesQuery, ResultOf<IEnumerable<PlayerInjuryReadModel>>>
    {
        private readonly IPlayerInjuryRepository _repository;

        public GetPlayerInjuriesQueryHandler(IPlayerInjuryRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultOf<IEnumerable<PlayerInjuryReadModel>>> Handle(GetPlayerInjuriesQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.GetReadModelsAsync(
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

            return items.AsPartial();
        }
    }
}
