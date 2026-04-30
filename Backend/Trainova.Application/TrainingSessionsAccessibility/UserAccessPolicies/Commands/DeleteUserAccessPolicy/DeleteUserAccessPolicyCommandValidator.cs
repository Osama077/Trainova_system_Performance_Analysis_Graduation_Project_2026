using FluentValidation;

namespace Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Commands.DeleteUserAccessPolicy
{
    public class DeleteUserAccessPolicyCommandValidator : AbstractValidator<DeleteUserAccessPolicyCommand>
    {
        public DeleteUserAccessPolicyCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
