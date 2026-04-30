using FluentValidation;

namespace Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Commands.UpdateUserAccessPolicy
{
    public class UpdateUserAccessPolicyCommandValidator : AbstractValidator<UpdateUserAccessPolicyCommand>
    {
        public UpdateUserAccessPolicyCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Status).IsInEnum().When(x => x.Status.HasValue);
            RuleFor(x => x.DoneScore).InclusiveBetween(0, 100).When(x => x.DoneScore.HasValue);
        }
    }
}
