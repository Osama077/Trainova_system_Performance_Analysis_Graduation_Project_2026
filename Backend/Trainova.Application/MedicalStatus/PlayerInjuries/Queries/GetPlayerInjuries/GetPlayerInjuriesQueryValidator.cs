using FluentValidation;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetPlayerInjuries
{
    public class GetPlayerInjuriesQueryValidator : AbstractValidator<GetPlayerInjuriesQuery>
    {
        private static readonly string[] AllowedSortColumns = new[]
        {
            "PlayerInjuryCreatedAt",
            "PlayerNumber",
            "PlayerId",
            "InjuryName",
            "Status",
            "HappendAt",
            "ExpectedReturnDate",
            "ReturnedAt"
        };

        public GetPlayerInjuriesQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(0).WithMessage("Page must be 0 or greater (0 is first page index used by API)");
            RuleFor(x => x.PageSize).InclusiveBetween(1, 200).WithMessage("PageSize must be between 1 and 200");

            RuleFor(x => x.SortDirection)
                .Must(d => d == null || d.Equals("ASC", StringComparison.OrdinalIgnoreCase) || d.Equals("DESC", StringComparison.OrdinalIgnoreCase))
                .WithMessage("SortDirection must be either 'ASC' or 'DESC'");

            RuleFor(x => x.SortColumn)
                .Must(c => c == null || AllowedSortColumns.Contains(c))
                .WithMessage($"SortColumn must be one of: {string.Join(',', AllowedSortColumns)}");
        }
    }
}
