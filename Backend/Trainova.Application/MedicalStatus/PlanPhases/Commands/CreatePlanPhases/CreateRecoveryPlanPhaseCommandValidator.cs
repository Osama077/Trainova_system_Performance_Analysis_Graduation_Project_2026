using FluentValidation;

namespace Trainova.Application.MedicalStatus.PlanPhases.Commands.CreatePlanPhases
{
    public class CreateRecoveryPlanPhaseCommandValidator : AbstractValidator<CreateRecoveryPlanPhaseCommand>
    {
        public CreateRecoveryPlanPhaseCommandValidator()
        {
            RuleFor(c => c.PlayerInjuryId).NotEmpty();
            RuleFor(c => c.Name).NotEmpty().MinimumLength(3).MaximumLength(150);
            RuleFor(c => c.Description).MinimumLength(3).MaximumLength(300).When(c => !string.IsNullOrEmpty(c.Description));
            RuleFor(c => c.Activities).Must(c => !c.Any(i => string.IsNullOrWhiteSpace(i))).When(c => c.Activities is not null);
            RuleFor(c => c.InsertOrder).GreaterThanOrEqualTo(0).When(c => c.InsertOrder is not null);

        }
    }
}
