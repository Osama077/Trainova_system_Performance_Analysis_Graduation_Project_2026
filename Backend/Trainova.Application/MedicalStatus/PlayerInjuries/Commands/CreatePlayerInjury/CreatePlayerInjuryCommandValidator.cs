// ...existing code...
using FluentValidation;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Commands.CreatePlayerInjury
{
    public class CreatePlayerInjuryCommandValidator : AbstractValidator<CreatePlayerInjuryCommand>
    {
        public CreatePlayerInjuryCommandValidator()
        {
            RuleFor(x => x.InjuryId).NotEmpty();
            RuleFor(x => x.PlayerId).NotEmpty();
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.Notes).MaximumLength(1200);
        }
    }
}
