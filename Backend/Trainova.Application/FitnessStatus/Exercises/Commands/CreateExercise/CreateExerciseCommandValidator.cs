using FluentValidation;

namespace Trainova.Application.FitnessStatus.Exercises.Commands.CreateExercise
{
    public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
    {
        public CreateExerciseCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Exercise name is required.")
                .MaximumLength(150).WithMessage("Exercise name cannot exceed 150 characters.");

            RuleFor(x => x.DefaultSets)
                .GreaterThan(0).WithMessage("Default sets must be greater than 0.");

            RuleFor(x => x.DefaultRepsOrDuration)
                .NotEmpty().WithMessage("Default repetitions or duration details are required.");

            RuleFor(x => x.DefaultRestBetweenSetsSec)
                .GreaterThanOrEqualTo(0).When(x => x.DefaultRestBetweenSetsSec.HasValue)
                .WithMessage("Rest time between sets cannot be negative.");

            RuleFor(x => x.RecoveryTimeHours)
                .GreaterThanOrEqualTo(0).When(x => x.RecoveryTimeHours.HasValue)
                .WithMessage("Recovery time cannot be negative.");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid exercise type selected.");

            RuleFor(x => x.Category)
                .IsInEnum().WithMessage("Invalid exercise category selected.");

            RuleFor(x => x.DefaultIntensity)
                .IsInEnum().WithMessage("Invalid default intensity selected.");

            RuleFor(x => x.TargetMuscleGroups)
                .NotEmpty().WithMessage("At least one target muscle group must be selected.");

            RuleFor(x => x.EquipmentRequired)
                .NotEmpty().WithMessage("At least one required equipment must be selected.");
        }
    }
}