using Trainova.Application.Profiles.Candidates.Queries.GetCandidates;

namespace Trainova.Api.Requsts.Profiles
{
    public class GetCandidatesFiltrationRequest
    {
        public string? SearchTerm { get; init; }
        public DateTime? DateFrom { get; init; }
        public DateTime? DateTo { get; init; }
        public int PageNumber { get; init; } = 0;
        public int PageSize { get; init; } = 12;
        public string SortColumn { get; init; } = "ScoutedAt";
        public string SortDirection { get; init; } = "DESC";

        public GetCandidatesQuery ToQuery()
        {
            return new GetCandidatesQuery()
            {
                SearchTerm = SearchTerm,
                DateFrom = DateFrom,
                DateTo = DateTo,
                PageNumber = PageNumber,
                PageSize = PageSize,
                SortColumn = SortColumn,
                SortDirection = SortDirection
            };
        }
    }
}
