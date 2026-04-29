// ...existing code...
using FluentValidation;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Commands.UpdatePlayerInjury
{
    public class UpdatePlayerInjuryCommandValidator : AbstractValidator<UpdatePlayerInjuryCommand>
    {
        public UpdatePlayerInjuryCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Notes).MaximumLength(1200);
        }
    }
}
