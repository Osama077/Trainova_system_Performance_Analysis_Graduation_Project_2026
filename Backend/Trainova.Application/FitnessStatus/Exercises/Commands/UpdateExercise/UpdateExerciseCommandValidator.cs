using FluentValidation;
using Trainova.Domain.FitnessStatus.Enums;

namespace Trainova.Application.FitnessStatus.Exercises.Commands.UpdateExercise
{
    public class UpdateExerciseCommandValidator : AbstractValidator<UpdateExerciseCommand>
    {
        public UpdateExerciseCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Exercise ID is required.");

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

            RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid exercise type selected.");
            RuleFor(x => x.Category).IsInEnum().WithMessage("Invalid exercise category selected.");
            RuleFor(x => x.DefaultIntensity).IsInEnum().WithMessage("Invalid default intensity selected.");

            RuleFor(x => x.TargetMuscleGroups)
                .NotEqual(MuscleGroup.None).WithMessage("At least one target muscle group must be selected.");

            RuleFor(x => x.EquipmentRequired)
                .NotEqual(EquipmentRequired.None).WithMessage("At least one required equipment must be selected.");
        }
    }
}