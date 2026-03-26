using System.ComponentModel.Design;
using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.MedicalStatus.Injuries;
using Trainova.Domain.Profiles.Players;

namespace Trainova.Domain.MedicalStatus.PlayerInjuries
{
    public class PlayerInjury : AuditableEntity<Guid>
    {
        public Guid InjuryId { get; private set; }
        public Injury Injury { get; private set; }
        public Guid PlayerId { get; private set; }
        public Player Player { get; private set; }
        public InjuryStatus Status { get; private set; } = InjuryStatus.InHealing;
        public InjuryCause Cause { get; private set; }
        public string? Notes { get; private set; }
        public bool IsNew { get; private set; }

        public DateTime? HappendAt { get; private set; }
        public DateTime? ReturnedAt { get; private set; }
        public DateTime? ExpectedReturnDate { get; private set; }



        public PlayerInjury(
            Guid injuryId,
            Guid playerId,
            InjuryStatus status,
            DateTime? happendAt = null,
            InjuryCause cause = default,
            string notes = null,
            bool isNew = false,
            Guid? createdBy = null) : base(createdBy)
        {
            InjuryId = injuryId;
            PlayerId = playerId;
            Status = status;
            HappendAt = happendAt ?? DateTime.UtcNow;
            Cause = cause;
            Notes = notes;
            IsNew = isNew;
        }
        private PlayerInjury() : base() { }

    }
}
