using FluentValidation;

namespace Trainova.Application.FitnessStatus.FitnessSessionExercises.Commands.UpdateFitnessSessionExercise
{
    public class UpdateFitnessSessionExerciseCommandValidator : AbstractValidator<UpdateFitnessSessionExerciseCommand>
    {
        public UpdateFitnessSessionExerciseCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required.");

            RuleFor(x => x.Sets)
                .GreaterThan(0).When(x => x.Sets.HasValue).WithMessage("Sets must be greater than 0.");

            RuleFor(x => x.RestTimeSec)
                .GreaterThanOrEqualTo(0).When(x => x.RestTimeSec.HasValue).WithMessage("Rest time cannot be negative.");

            RuleFor(x => x.Intensity)
                .IsInEnum().When(x => x.Intensity.HasValue).WithMessage("Invalid intensity selected.");
        }
    }
}
