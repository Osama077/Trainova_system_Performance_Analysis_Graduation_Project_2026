using FluentValidation;

namespace Trainova.Application.Profiles.Commands.CreatePlayerProfile;

public class CreatePlayerProfileCommandValidator : AbstractValidator<CreatePlayerProfileCommand>
{
    public CreatePlayerProfileCommandValidator()
    {
        RuleFor(v => v.ShowName).NotEmpty();
        RuleFor(v => v.FullName).NotEmpty();
        RuleFor(v => v.Password).NotEmpty();
        RuleFor(v => v.PlayerNumber).GreaterThan(0);
        RuleFor(v => v.TShirtName).NotEmpty();
    }
}
