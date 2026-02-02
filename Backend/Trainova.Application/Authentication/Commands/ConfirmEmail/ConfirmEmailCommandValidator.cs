using FluentValidation;

namespace Trainova.Application.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandValidator:
        AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Token)
                .NotEmpty()
                .MaximumLength(6).MinimumLength(6);
        }
    }
}
