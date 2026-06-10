using FluentValidation;

namespace Trainova.Application.Scouting.Candidates.Commands.AddSeasonStatistics
{
    public class AddSeasonStatisticsCommandValidator : AbstractValidator<AddSeasonStatisticsCommand>
    {
        public AddSeasonStatisticsCommandValidator()
        {
            RuleFor(x => x.Season)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.League)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Goals)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Assists)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Matches)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.PassAccuracy)
                .InclusiveBetween(0f, 100f)
                .WithMessage("Pass accuracy must be between 0 and 100.");

            RuleFor(x => x.ShotsPer90)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.XgPer90)
                .GreaterThanOrEqualTo(0);
        }
    }
}
