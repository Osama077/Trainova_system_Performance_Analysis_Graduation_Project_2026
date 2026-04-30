using FluentValidation;

namespace Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Commands.UpdateAccessPolicy
{
    public class UpdateAccessPolicyCommandValidator : AbstractValidator<UpdateAccessPolicyCommand>
    {
        public UpdateAccessPolicyCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.PolicyName).MaximumLength(200).When(x => x.PolicyName != null);
        }
    }
}
