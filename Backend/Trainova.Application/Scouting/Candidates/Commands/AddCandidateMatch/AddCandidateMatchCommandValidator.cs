using FluentValidation;

namespace Trainova.Application.Scouting.Candidates.Commands.AddCandidateMatch
{
    public class AddCandidateMatchCommandValidator : AbstractValidator<AddCandidateMatchCommand>
    {
        public AddCandidateMatchCommandValidator()
        {
            RuleFor(x => x.MatchName)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Rating)
                .InclusiveBetween(0f, 10.0f)
                .WithMessage("Match rating must be between 0.0 and 10.0.");

            RuleFor(x => x.Goals)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Assists)
                .GreaterThanOrEqualTo(0);
        }
    }
}
