using FluentValidation;

namespace Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Commands.CreateUserAccessPolicy
{
    public class CreateUserAccessPolicyCommandValidator : AbstractValidator<CreateUserAccessPolicyCommand>
    {
        public CreateUserAccessPolicyCommandValidator()
        {
            RuleFor(x => x.AccessPolicyId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.InitialStatus).IsInEnum();
        }
    }
}
