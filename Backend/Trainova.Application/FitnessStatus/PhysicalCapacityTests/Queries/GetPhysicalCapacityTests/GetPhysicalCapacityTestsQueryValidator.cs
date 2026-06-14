using FluentValidation;

namespace Trainova.Application.FitnessStatus.PhysicalCapacityTests.Queries.GetPhysicalCapacityTests
{
    public class GetPhysicalCapacityTestsQueryValidator : AbstractValidator<GetPhysicalCapacityTestsQuery>
    {
        public GetPhysicalCapacityTestsQueryValidator()
        {
            RuleFor(x => x.FromDate)
                .LessThanOrEqualTo(x => x.ToDate)
                .When(x => x.FromDate.HasValue && x.ToDate.HasValue)
                .WithMessage("From date cannot be after to date.");

            RuleFor(x => x.PlayerId)
                .NotEmpty()
                .When(x => x.PlayerId.HasValue)
                .WithMessage("Invalid PlayerId format.");
        }
    }
}
