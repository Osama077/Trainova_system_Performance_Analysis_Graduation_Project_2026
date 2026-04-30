using FluentValidation;

namespace Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Commands.DeleteAccessPolicy
{
    public class DeleteAccessPolicyCommandValidator : AbstractValidator<DeleteAccessPolicyCommand>
    {
        public DeleteAccessPolicyCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
