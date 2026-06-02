using System.Collections.Generic;

namespace Trainova.Application.Scouting.Candidates
{
    public class OverviewCounts
    {
        public int TotalCandidates { get; set; }
        public int Shortlisted { get; set; }
        public int PlayersSigned { get; set; }
    }

    public class CandidatesOverviewResponse
    {
        public OverviewCounts Counts { get; set; } = new OverviewCounts();
        public IEnumerable<CandidateListItemResponse> Items { get; set; } = new List<CandidateListItemResponse>();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
