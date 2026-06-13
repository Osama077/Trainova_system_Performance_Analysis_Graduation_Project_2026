using FluentValidation;

namespace Trainova.Application.FitnessStatus.FitnessSessionExercises.Commands.CreateFitnessSessionExercise
{
    public class CreateFitnessSessionExerciseCommandValidator : AbstractValidator<CreateFitnessSessionExerciseCommand>
    {
        public CreateFitnessSessionExerciseCommandValidator()
        {
            RuleFor(x => x.SessionId).NotEmpty().WithMessage("Session ID is required.");
            RuleFor(x => x.ExerciseId).NotEmpty().WithMessage("Exercise ID is required.");

            RuleFor(x => x.Sets)
                .GreaterThan(0).When(x => x.Sets.HasValue).WithMessage("Sets must be greater than 0.");

            RuleFor(x => x.RestTimeSec)
                .GreaterThanOrEqualTo(0).When(x => x.RestTimeSec.HasValue).WithMessage("Rest time cannot be negative.");

            RuleFor(x => x.Intensity)
                .IsInEnum().When(x => x.Intensity.HasValue).WithMessage("Invalid intensity selected.");
        }
    }
}
