using FluentValidation;
using Trainova.Application.MedicalStatus.Common;

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
            RuleFor(x => x.InjuryType)
                .NotEmpty()
                .Must(type => InjuryValues.AllowedInjuryTypes.Contains(type))
                .WithMessage("InjuryType must be either 'Muscular' or 'Bone'.");
            RuleFor(x => x.TimeType)
                .NotEmpty()
                .Must(timeType => InjuryValues.AllowedTimeTypes.Contains(timeType))
                .WithMessage(InjuryValues.MsgAllowedTimeTypes);
            RuleFor(x => x.TimeAmount).GreaterThan(0);
        }
    }

}