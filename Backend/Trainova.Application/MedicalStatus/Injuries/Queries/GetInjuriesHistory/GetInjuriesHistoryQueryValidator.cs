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
            RuleFor(x => x).Must(x => x.IncludeDeleted || x.IncludeUpdated || x.IncludeAdded)
                .WithMessage("At least one audit type should be want.");
        }

    }

}