using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus.Injuries;

namespace Trainova.Application.MedicalStatus.Injuries.Queries.GetInjuries
{
    [Authorize]
    public record GetInjuriesQuery(
        string? InjuryType)
        : IRequest<ResultOf<IEnumerable<Injury>>>;


}
