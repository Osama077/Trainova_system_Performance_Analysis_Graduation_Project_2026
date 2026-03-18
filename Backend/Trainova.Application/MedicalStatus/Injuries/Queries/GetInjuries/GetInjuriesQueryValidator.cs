using FluentValidation;
using Trainova.Application.MedicalStatus.Common;

namespace Trainova.Application.MedicalStatus.Injuries.Queries.GetInjuries
{
    public class GetInjuriesQueryValidator : AbstractValidator<GetInjuriesQuery>
    {
        public GetInjuriesQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().When(x => x.Id.HasValue);
            RuleFor(x => x.InjuryType)
                .Must(t=>InjuryValues.AllowedInjuryTypes.Contains(t))
                .When(x => x.InjuryType is not null)
                .WithMessage(InjuryValues.MsgAllowedInjuryTypes);
            RuleFor(x => x.Page).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PageSize).GreaterThan(0).LessThanOrEqualTo(100);
        }

    }

}
