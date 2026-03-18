using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.MedicalStatus.Injuries;
using Trainova.Domain.Profiles.Players;

namespace Trainova.Domain.MedicalStatus.PlayerInjuries
{
    public class PlayerInjury : IAuditable<Guid>
    {
        public Guid Id { get; private set; }
        public Guid InjuryId { get; private set; }
        public Injury Injury { get; private set; }
        public Guid PlayerId { get; private set; }
        public Player Player { get; private set; }
        public InjuryStatus Status { get; private set; } = InjuryStatus.InHealing;
        public InjuryCause Cause { get; private set; }
        public string Notes { get; private set; }
        public bool IsNew { get; private set; }

        public DateTime? HappendAt { get; private set; }
        public DateTime? ReturnedAt { get; private set; }
        public DateTime? ExpectedReturnDate { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        public Guid? CreatedBy { get; private set; }
        object IAuditable.Id => Id;

        public PlayerInjury(Guid injuryId, Guid playerId, InjuryStatus status, DateTime? happendAt = null)
        {
            Id = Guid.NewGuid();
            InjuryId = injuryId;
            PlayerId = playerId;
            Status = status;
            HappendAt = happendAt ?? DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
            LastUpdate = null;
        }
        private PlayerInjury() { }

    }
}
