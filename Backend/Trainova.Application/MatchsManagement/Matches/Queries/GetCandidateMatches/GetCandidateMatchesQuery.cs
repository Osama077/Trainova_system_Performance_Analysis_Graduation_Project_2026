using MediatR;
using Trainova.Common.ResultOf;

namespace Trainova.Application.MatchsManagement.Matches.Queries.GetCandidateMatches
{
    public record GetCandidateMatchesQuery : IRequest<ResultOf<IEnumerable<Trainova.Application.MatchsManagement.Matches.CandidateMatchResponse>>>
    {
        public Guid? CandidateId { get; init; }
        public DateTime? DateFrom { get; init; }
        public DateTime? DateTo { get; init; }
        public int PageNumber { get; init; } = 0;
        public int PageSize { get; init; } = 12;
    }
}
