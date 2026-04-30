using FluentValidation;
using Trainova.Domain.TrainingSessionsAccessibility.Plans;
using Trainova.Domain.Common.Enums;

namespace Trainova.Application.TrainingSessionsAccessibility.Plans.Commands.CreatePlan
{
    public class CreatePlanCommandValidator : AbstractValidator<CreatePlanCommand>
    {
        public CreatePlanCommandValidator()
        {
            RuleFor(x => x.PlanName).NotEmpty().WithMessage("Plan name is required.").MaximumLength(200);
            RuleFor(x => x.PlanGoal).NotEmpty().WithMessage("Plan goal is required.").MaximumLength(1200);
            RuleFor(x => x.AccessPolicyId).NotEmpty().WithMessage("AccessPolicyId is required.");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("StartDate is required.");
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).When(x => x.EndDate.HasValue)
                .WithMessage("EndDate must be after StartDate.");
        }
    }
}
