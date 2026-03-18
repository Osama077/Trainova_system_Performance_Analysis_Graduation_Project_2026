using FluentValidation;
using Trainova.Application.MedicalStatus.Common;

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
            RuleFor(x => x.InjuryType)
                .NotEmpty()
                .Must(type => InjuryValues.AllowedInjuryTypes.Contains(type))
                .WithMessage("InjuryType must be either 'Muscular' or 'Bone'.")
                .When(x => x.InjuryType is not null);
            RuleFor(x => x.TimeType)
                .NotEmpty()
                .Must(timeType => InjuryValues.AllowedTimeTypes.Contains(timeType))
                .WithMessage(InjuryValues.MsgAllowedTimeTypes)
                .When(x => x.TimeType is not null);
            RuleFor(x => x.TimeAmount).GreaterThan(0)
                .When(x => x.TimeAmount.HasValue);
            RuleFor(x => x).Must(x => (String.IsNullOrEmpty(x.TimeType) && !x.TimeAmount.HasValue) ||
                                 (!String.IsNullOrEmpty(x.TimeType) && x.TimeAmount.HasValue))
                .WithMessage("TimeType and TimeAmount must be provided together.");
        }
    }

}
