using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Domain.Profiles
{
    public class Candidate : AuditableEntity<Guid>
    {
        public string FullName { get; private set; }
        public string? Email { get; private set; }
        public DateTime ScoutedAt { get; private set; }
        public bool IsShortlisted { get; private set; } = false;

        private Candidate() : base() { }

        public Candidate(
            Guid id,
            string fullName,
            DateTime scoutedAt,
            string? email = null,
            Guid? createdBy = null) : base(id, createdBy)
        {
            FullName = fullName ?? throw new DomainException("FullName is required.", "DomainError_FullNameRequired");
            Email = email;
            ScoutedAt = scoutedAt;
        }

        public void Update(string? fullName = null, DateTime? scoutedAt = null, string? email = null)
        {
            MarkUpdatedNow();
            FullName = fullName ?? FullName;
            ScoutedAt = scoutedAt ?? ScoutedAt;
            Email = email ?? Email;
        }

        public void SetShortlisted(bool isShortlisted)
        {
            MarkUpdatedNow();
            IsShortlisted = isShortlisted;
        }
    }
}
