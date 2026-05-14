using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.MedicalStatus.Injuries.Queries.GetInjuries
{
    [Authorize(Roles = "Doctor")]
    public record GetInjuriesQuery(
        string? SearchTerm,
        string? InjuryType)
        : IRequest<ResultOf<IEnumerable<Injury>>>;


}
