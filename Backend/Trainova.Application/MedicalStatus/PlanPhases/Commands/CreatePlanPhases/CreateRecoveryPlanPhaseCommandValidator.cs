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
            RuleFor(c => c.To).NotEmpty().GreaterThan(DateTime.Now);
            RuleFor(c => c.From).NotEmpty().LessThan(c => c.To).When(c=> c.From.HasValue);
            RuleFor(c => c.Activities).Must(c=>!c.Any(i=>string.IsNullOrWhiteSpace(i))).When(c=>c.Activities is not null);
        }
    }
}
