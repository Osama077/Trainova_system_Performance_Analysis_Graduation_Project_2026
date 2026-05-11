using FluentValidation;

namespace Trainova.Application.MedicalStatus.Injuries.Commands.UpdateInjury
{
    public class UpdateInjuryCommandValidator : AbstractValidator<UpdateInjuryCommand>
    {

        public UpdateInjuryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .MaximumLength(100).When(x=>x.Name is not null);
            RuleFor(x => x.Description)
                .MaximumLength(500).When(x => x.Description is not null);


            RuleFor(x => x.TimeAmountInDayes).GreaterThan(0)
                .When(x => x.TimeAmountInDayes.HasValue);

        }
    }

}
