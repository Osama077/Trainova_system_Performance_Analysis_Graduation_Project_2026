using FluentValidation;

namespace Trainova.Application.MedicalStatus.Injuries.Commands.CreateInjury
{
    public class CreateInjuryCommandValidator : AbstractValidator<CreateInjuryCommand>
    {

        public CreateInjuryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Description)
                .MaximumLength(500);


            RuleFor(x => x.TimeAmountInDayes).GreaterThan(0);
        }
    }

}