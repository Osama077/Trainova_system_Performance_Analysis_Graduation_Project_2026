using FluentValidation;

namespace Trainova.Application.MedicalStatus.Injuries.Queries.GetInjuriesHistory
{
    public class GetInjuriesHistoryQueryValidator : AbstractValidator<GetInjuriesHistoryQuery>
    {
        public GetInjuriesHistoryQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
            RuleFor(x => x.Id).NotEmpty().When(x=>x.Id.HasValue);
        }

    }

}