using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Application.Common.Interfaces.Repositories.CommonRepos;
using Trainova.Common.ResultOf;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetPlayerInjuriesTrend
{
    [Authorize(Roles = "Doctor,HeadCoach,FitnessCoach,AssistantCoach")]
    public record GetPlayerInjuriesTrendQuery(
        Guid? PlayerId,
        Guid? InjuryId,
        string? Status,
        string? Cause,
        bool? IsNew,
        DateTime? HappendBefore,
        DateTime? HappendAfter,
        DateTime? ExpectedReturnBefore,
        DateTime? ExpectedReturnAfter,
        DateTime? ReturnedBefore,
        DateTime? ReturnedAfter
        ):IRequest<ResultOf<PlayerInjuriesTrendResult>>;
}
