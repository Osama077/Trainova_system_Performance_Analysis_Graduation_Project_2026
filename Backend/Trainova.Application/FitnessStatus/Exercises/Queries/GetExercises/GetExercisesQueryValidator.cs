using FluentValidation;

namespace Trainova.Application.FitnessStatus.Exercises.Queries.GetExercises
{
    public class GetExercisesQueryValidator : AbstractValidator<GetExercisesQuery>
    {
        public GetExercisesQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Page number must be greater than or equal to 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("Page size must be greater than 0.")
                .LessThanOrEqualTo(100)
                .WithMessage("Page size cannot exceed 100 items per page.");

            RuleFor(x => x.SortBy)
                .Must(value => string.IsNullOrEmpty(value) ||
                               value == "Name" ||
                               value == "Type" ||
                               value == "DefaultExerciseIntensity" ||
                               value == "CreatedAt")
                .WithMessage("Sort field must be one of the following: Name, Type, DefaultExerciseIntensity, CreatedAt.");

            RuleFor(x => x.SortDir)
                .Must(value => string.IsNullOrEmpty(value) ||
                               value.Equals("ASC", StringComparison.OrdinalIgnoreCase) ||
                               value.Equals("DESC", StringComparison.OrdinalIgnoreCase))
                .WithMessage("Sort direction must be either 'ASC' or 'DESC'.");
        }
    }
}
