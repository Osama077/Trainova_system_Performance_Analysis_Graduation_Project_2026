using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus.Injuries;

namespace Trainova.Application.MedicalStatus.Injuries.Queries.GetInjuries
{
    public record GetInjuriesQuery(
        Guid? Id,
        string? InjuryType,
        int Page = 0,
        int PageSize = 12)
        : IRequest<ResultOf<IEnumerable<Injury>>>;


}
