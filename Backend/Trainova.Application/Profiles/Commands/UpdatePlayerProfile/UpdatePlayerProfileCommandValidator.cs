using FluentValidation;

namespace Trainova.Application.Profiles.Commands.UpdatePlayerProfile;

public class UpdatePlayerProfileCommandValidator : AbstractValidator<UpdatePlayerProfileCommand>
{
    public UpdatePlayerProfileCommandValidator()
    {
        RuleFor(v => v.PlayerId).NotEmpty();
        RuleFor(v => v.PlayerNumber).GreaterThan(0).When(v => v.PlayerNumber.HasValue);
    }
}
