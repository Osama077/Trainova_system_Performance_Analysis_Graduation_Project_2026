using MediatR;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Profiles.Candidates.Queries.GetCandidates
{
    public record GetCandidatesQuery : IRequest<ResultOf<IEnumerable<Trainova.Application.Profiles.Candidates.CandidateDetailResponse>>>
    {
        public Guid? CandidateId { get; init; }
        public string? SearchTerm { get; init; }
        public DateTime? DateFrom { get; init; }
        public DateTime? DateTo { get; init; }
        public int PageNumber { get; init; } = 0;
        public int PageSize { get; init; } = 12;
        public string SortColumn { get; init; } = "ScoutedAt";
        public string SortDirection { get; init; } = "DESC";

        public GetCandidatesQuery() { }
    }
}
