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
                .NotEmpty().WithMessage("Exercise name cannot be empty.")
                .MaximumLength(150).WithMessage("Exercise name cannot exceed 150 characters.")
                .When(x => x.Name != null);

            RuleFor(x => x.DefaultSets)
                .GreaterThan(0).WithMessage("Default sets must be greater than 0.")
                .When(x => x.DefaultSets.HasValue);

            RuleFor(x => x.DefaultRepsOrDuration)
                .NotEmpty().WithMessage("Default repetitions or duration details cannot be empty.")
                .When(x => x.DefaultRepsOrDuration != null);

            RuleFor(x => x.DefaultRestBetweenSetsSec)
                .GreaterThanOrEqualTo(0).WithMessage("Rest time between sets cannot be negative.")
                .When(x => x.DefaultRestBetweenSetsSec.HasValue);

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid exercise type selected.")
                .When(x => x.Type.HasValue);

            RuleFor(x => x.Category)
                .IsInEnum().WithMessage("Invalid exercise category selected.")
                .When(x => x.Category.HasValue);

            RuleFor(x => x.DefaultIntensity)
                .IsInEnum().WithMessage("Invalid default intensity selected.")
                .When(x => x.DefaultIntensity.HasValue);

            RuleFor(x => x.TargetMuscleGroups)
                .NotEqual(MuscleGroup.None).WithMessage("At least one target muscle group must be selected.")
                .When(x => x.TargetMuscleGroups.HasValue);

            RuleFor(x => x.EquipmentRequired)
                .NotEqual(EquipmentRequired.None).WithMessage("At least one required equipment must be selected.")
                .When(x => x.EquipmentRequired.HasValue);
        }
    }
}