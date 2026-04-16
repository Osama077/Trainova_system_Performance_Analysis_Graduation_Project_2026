using FluentValidation;
using Trainova.Domain.UserAuth.UserTokens;

namespace Trainova.Application.Authentication.Commands.CreateToken;

public class CreateTokenCommandValidator : AbstractValidator<CreateTokenCommand>
{
    private readonly static IReadOnlyCollection<string> AllowedTypes = new[]
        {
            TokenType.RefreshToken.Name,
            TokenType.EmailConfirmation.Name,
            TokenType.PasswordReset.Name
        };
    public CreateTokenCommandValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty()
            .Must(x => AllowedTypes.Contains(x))
            .WithMessage($"Type must be either '{TokenType.RefreshToken.Name}' or '{TokenType.EmailConfirmation.Name}' or '{TokenType.PasswordReset.Name}'.");
        When(x => x.Type == "passwordreset", () =>
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("A valid email is required for password reset.");
        });
    }
}




