using FluentValidation;
using Trainova.Application.Common.Helpers;
using Trainova.Application.Profiles.Players.Common;

namespace Trainova.Application.Profiles.Players.Queries.GetPlayersProfiles
{
    public class GetPlayersProfilesQueryValidator
        : AbstractValidator<GetPlayersProfileQuery>
    {
        public GetPlayersProfilesQueryValidator()
        {
            // =========================
            // Pagination
            // =========================

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(0)
                .WithMessage("PageNumber must be greater than or equal to 0.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("PageSize must be between 1 and 100.");

            // =========================
            // Sorting
            // =========================

            RuleFor(x => x.SortColumn)
                .NotEmpty()
                .Must(BeValidSortColumn)
                .WithMessage($"Invalid SortColumn. Allowed values: {string.Join(", ", PlayerCommonOptions.ValidSortColumns)}");

            RuleFor(x => x.SortDirection)
                .NotEmpty()
                .Must(BeValidSortDirection)
                .WithMessage("SortDirection must be ASC or DESC.");

            // =========================
            // Date Range
            // =========================

            RuleFor(x => x)
                .Must(BeValidDateRange)
                .WithMessage("DateFrom must be less than or equal to DateTo.");

            // =========================
            // Numeric Filters
            // =========================

            RuleFor(x => x.MinMatches)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MinMatches.HasValue);

            RuleFor(x => x.PerformanceLevel)
                .InclusiveBetween(0, 10)
                .When(x => x.PerformanceLevel.HasValue);

            RuleFor(x => x.MainPositionFilter)
                .GreaterThan(0)
                .When(x => x.MainPositionFilter.HasValue);

            RuleFor(x => x.OtherPositionFilter)
                .GreaterThan(0)
                .When(x => x.OtherPositionFilter.HasValue);

            // =========================
            // SearchTerm
            // =========================

            RuleFor(x => x.SearchTerm)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.SearchTerm));

            // =========================
            // MedicalStatus
            // =========================

            RuleFor(x => x.MedicalStatus)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.MedicalStatus));
        }

        // =========================
        // Helpers
        // =========================

        private static bool BeValidSortColumn(string column)
        {
            return PlayerCommonOptions.ValidSortColumns
                .Contains(column);
        }

        private static bool BeValidSortDirection(string direction)
        {
            return GeneralSortHelper.SortDirectionOptions
                .Contains(direction?.ToUpper());
        }

        private static bool BeValidDateRange(GetPlayersProfileQuery query)
        {
            if (!query.DateFrom.HasValue || !query.DateTo.HasValue)
                return true;

            return query.DateFrom <= query.DateTo;
        }
    }

}
