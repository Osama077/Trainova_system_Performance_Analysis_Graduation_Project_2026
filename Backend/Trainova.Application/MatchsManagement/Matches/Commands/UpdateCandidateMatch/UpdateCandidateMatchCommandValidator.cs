using FluentValidation;

namespace Trainova.Application.MatchsManagement.Matches.Commands.UpdateCandidateMatch
{
    public class UpdateCandidateMatchCommandValidator : AbstractValidator<UpdateCandidateMatchCommand>
    {
        public UpdateCandidateMatchCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.OpponentName).MaximumLength(200).When(x => x.OpponentName != null);
            RuleFor(x => x.HomeScore).GreaterThanOrEqualTo(0).When(x => x.HomeScore.HasValue);
            RuleFor(x => x.AwayScore).GreaterThanOrEqualTo(0).When(x => x.AwayScore.HasValue);
            RuleFor(x => x.Notes).MaximumLength(2000).When(x => !string.IsNullOrEmpty(x.Notes));
        }
    }
}
