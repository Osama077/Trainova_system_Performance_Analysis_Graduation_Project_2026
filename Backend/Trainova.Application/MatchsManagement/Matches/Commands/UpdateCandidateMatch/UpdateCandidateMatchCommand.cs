using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Domain.MatchsManagement.Matches;

namespace Trainova.Application.MatchsManagement.Matches.Commands.UpdateCandidateMatch
{
    public record UpdateCandidateMatchCommand(
        Guid Id,
        Guid? CandidateId,
        DateTime? MatchDate,
        string? OpponentName,
        int? HomeScore,
        int? AwayScore,
        string? Notes)
        : IRequest<ResultOf<CandidateMatch>>;
}
