using FluentValidation;

namespace Trainova.Application.FitnessStatus.SessionMovements.Queries.PlayerLoadVsCapacityTimeline
{
    public class GetPlayerLoadVsCapacityTimelineQueryValidator : AbstractValidator<GetPlayerLoadVsCapacityTimelineQuery>
    {
        public GetPlayerLoadVsCapacityTimelineQueryValidator()
        {
            RuleFor(x => x.PlayerId)
                .NotEmpty()
                .WithMessage("PlayerId is required to fetch custom timeline data.");

            RuleFor(x => x.FromDate)
                .LessThanOrEqualTo(x => x.ToDate)
                .When(x => x.FromDate.HasValue && x.ToDate.HasValue)
                .WithMessage("From date cannot be after to date.");
        }
    }

}
