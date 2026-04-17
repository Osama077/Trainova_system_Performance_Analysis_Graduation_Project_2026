using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus.PlayerInjuries;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetPlayerInjuries
{
    public record GetPlayerInjuriesQuery(
            Guid? PlayerInjuryId = null,
            Guid? PlayerId = null,
            Guid? InjuryId = null,
            string? Status = null,
            string? Cause = null,
            bool? IsNew = null,
            DateTime? HappendBefore = null,
            DateTime? HappendAfter = null,
            DateTime? ExpectedReturnBefore = null,
            DateTime? ExpectedReturnAfter = null,
            DateTime? ReturnedBefore = null,
            DateTime? ReturnedAfter = null,
            int Page = 0,
            int PageSize = 12
            ) : IRequest<ResultOf<IEnumerable<PlayerInjury>>>;

}
