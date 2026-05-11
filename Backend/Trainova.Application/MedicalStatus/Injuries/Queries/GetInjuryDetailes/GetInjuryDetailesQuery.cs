using FluentValidation;
using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.MedicalStatus.Injuries.Queries.GetInjuryDetailes
{
    [Authorize(Role = "Doctor,SystemAdmin,HeadCoach")]
    public record GetInjuryDetailesQuery(Guid Id) : IRequest<ResultOf<InjuryDetailes>>;
    public class GetInjuryDetailesQueryValidator : AbstractValidator<GetInjuryDetailesQuery>
    {
        public GetInjuryDetailesQueryValidator()
        {
            RuleFor(q => q.Id).NotEmpty();
        }
    }
}
