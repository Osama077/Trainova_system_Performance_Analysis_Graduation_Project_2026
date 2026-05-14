using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetCasesCount
{
    [Authorize(Roles = "Admin,HeadCoach,Doctor,FitnessCoach,AssistantCoach")]
    public record GetInjuriesCasesCountOverDayesQuery(
        Guid? InjuryId = null,
        int Days = 7) : IRequest<ResultOf<CasesCountResponse>>;


}
