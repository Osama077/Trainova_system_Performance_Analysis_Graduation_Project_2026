using FluentValidation;

namespace Trainova.Application.MatchsManagement.Matches.Commands.CreateCandidateMatch
{
    public class CreateCandidateMatchCommandValidator : AbstractValidator<CreateCandidateMatchCommand>
    {
        public CreateCandidateMatchCommandValidator()
        {
            RuleFor(x => x.CandidateId).NotEmpty();
            RuleFor(x => x.MatchDate).NotEmpty();
            RuleFor(x => x.OpponentName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.HomeScore).GreaterThanOrEqualTo(0).When(x => x.HomeScore.HasValue);
            RuleFor(x => x.AwayScore).GreaterThanOrEqualTo(0).When(x => x.AwayScore.HasValue);
            RuleFor(x => x.Notes).MaximumLength(2000).When(x => !string.IsNullOrEmpty(x.Notes));
        }
    }
}
