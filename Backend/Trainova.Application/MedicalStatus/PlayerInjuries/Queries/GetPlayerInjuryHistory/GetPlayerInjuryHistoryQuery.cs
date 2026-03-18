using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus.PlayerInjuries;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetPlayerInjuryHistory
{
    public record GetPlayerInjuryHistoryQuery(
        Guid? PlayerInjuryId = null,
        int Page = 0,
        int PageSize = 12,
        bool IncludeAdded = false,
        bool IncludeDeleted = false,
        bool IncludeUpdated = false) : IRequest<ResultOf<IEnumerable<PlayerInjury>>>;

}