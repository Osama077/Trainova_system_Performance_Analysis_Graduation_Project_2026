using System;

namespace Trainova.Application.Scouting.Candidates.Queries.GetCandidateNotes
{
    public class CandidateNoteResponse
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public Guid? CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalCount { get; set; }
    }
}
