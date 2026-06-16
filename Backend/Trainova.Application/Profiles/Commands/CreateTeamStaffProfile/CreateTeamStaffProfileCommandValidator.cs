using FluentValidation;

namespace Trainova.Application.Profiles.Commands.CreateTeamStaffProfile;

public class CreateTeamStaffProfileCommandValidator : AbstractValidator<CreateTeamStaffProfileCommand>
{
    public CreateTeamStaffProfileCommandValidator()
    {
        RuleFor(v => v.ShowName).NotEmpty();
        RuleFor(v => v.FullName).NotEmpty();
        RuleFor(v => v.Password).NotEmpty();
        RuleFor(v => v.Role).IsInEnum();
    }
}
