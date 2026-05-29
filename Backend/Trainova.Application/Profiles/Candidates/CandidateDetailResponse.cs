using Trainova.Application.Common.Interfaces.MarkUps;

namespace Trainova.Application.Profiles.Candidates
{
    public class CandidateDetailResponse : ITotalCountIncluded
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string? Email { get; set; }
        public DateTime ScoutedAt { get; set; }
        public bool IsShortlisted { get; set; }

        // Captured from COUNT(*) OVER()
        private int TotalCount { get; set; }
        public CandidateDetailResponse() { }
        int ITotalCountIncluded.TotalCount => TotalCount;
    }
}
