using System;

namespace Trainova.Domain.Scouting
{
    public class ScoutingCandidateNote
    {
        public Guid Id { get; private set; }
        public Guid ScoutingCandidateId { get; private set; }
        public string Text { get; private set; } = string.Empty;
        public Guid? CreatedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private ScoutingCandidateNote() { }

        public ScoutingCandidateNote(Guid scoutingCandidateId, string text, Guid? createdBy)
        {
            Id = Guid.NewGuid();
            ScoutingCandidateId = scoutingCandidateId;
            Text = text ?? string.Empty;
            CreatedBy = createdBy;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
