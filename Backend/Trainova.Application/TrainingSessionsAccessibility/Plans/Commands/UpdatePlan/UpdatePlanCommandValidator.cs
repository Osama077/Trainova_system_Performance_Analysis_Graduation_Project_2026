using FluentValidation;

namespace Trainova.Application.TrainingSessionsAccessibility.Plans.Commands.UpdatePlan
{
    public class UpdatePlanCommandValidator : AbstractValidator<UpdatePlanCommand>
    {
        public UpdatePlanCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.PlanName).MaximumLength(200).When(x => x.PlanName != null);
            RuleFor(x => x.PlanGoal).MaximumLength(1200).When(x => x.PlanGoal != null);
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).When(x => x.EndDate.HasValue && x.StartDate.HasValue);
        }
    }
}
