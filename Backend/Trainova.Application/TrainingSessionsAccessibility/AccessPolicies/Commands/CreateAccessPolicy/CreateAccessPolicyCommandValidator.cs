using FluentValidation;

namespace Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Commands.CreateAccessPolicy
{
    public class CreateAccessPolicyCommandValidator : AbstractValidator<CreateAccessPolicyCommand>
    {
        public CreateAccessPolicyCommandValidator()
        {
            RuleFor(x => x.PolicyName).NotEmpty().MaximumLength(200);
        }
    }
}
