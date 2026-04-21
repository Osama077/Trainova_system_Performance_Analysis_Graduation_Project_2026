using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus.PlayerInjuries;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetPlayerInjuryHistory
{
    [Authorize(Role = "Doctor,Player,SystemAdmin,HeadCoach,AssistantCoach")]
    public record GetPlayerInjuryHistoryQuery(
        Guid? PlayerInjuryId = null,
        int Page = 0,
        int PageSize = 12,
        bool IncludeAdded = false,
        bool IncludeDeleted = false,
        bool IncludeUpdated = false) : IRequest<ResultOf<IEnumerable<PlayerInjury>>>;

}