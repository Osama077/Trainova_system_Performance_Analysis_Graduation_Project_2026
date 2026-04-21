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
        public SevertiyGrade SevertiyGrade { get; private set; }
        public string BodyPart { get; private set; }
        public string? Notes { get; private set; }
        public bool IsNew { get; private set; }

        public DateTime? HappendAt { get; private set; }
        public DateTime? ReturnedAt { get; private set; }
        public DateTime? ExpectedReturnDate { get; private set; }



        public PlayerInjury(
            Guid playerId,
            Guid injuryId,
            InjuryStatus status,
            DateTime? happendAt = null,
            InjuryCause cause = default,
            SevertiyGrade severtiyGrade = default,
            string bodyPart = null,
            string notes = null,
            bool isNew = false,
            DateTime? expectedReturnDate = null,
            Guid? createdBy = null) : base(createdBy)
        {
            PlayerId = playerId;
            InjuryId = injuryId;
            Status = status;
            HappendAt = happendAt ?? DateTime.UtcNow;
            Cause = cause;
            SevertiyGrade = severtiyGrade;
            BodyPart = bodyPart;
            Notes = notes;
            IsNew = isNew;
            ExpectedReturnDate = expectedReturnDate;
        }

        public void Update(
            DateTime? happendAt = null,
            InjuryCause? cause = null,
            SevertiyGrade? severtiyGrade = null,
            string bodyPart = null,
            string notes = null,
            bool? isNew = null,
            InjuryStatus? newStatus = null,
            DateTime? returnedAt = null,
            DateTime? expectedReturnDate = null)
        {
            MarkUpdatedNow();
            HappendAt = happendAt ?? HappendAt;
            Cause = cause ?? Cause;
            SevertiyGrade = severtiyGrade ?? SevertiyGrade;
            BodyPart = bodyPart ?? BodyPart;
            Notes = notes ?? Notes;
            IsNew = isNew ?? IsNew;
            ExpectedReturnDate = expectedReturnDate ?? ExpectedReturnDate;
            if (newStatus.HasValue)
            {
                Status = newStatus.Value;
                if (newStatus == InjuryStatus.Ended)
                {
                    ReturnedAt = returnedAt ?? DateTime.UtcNow;
                }
            }
        }


        private PlayerInjury() : base() { }

    }
}
