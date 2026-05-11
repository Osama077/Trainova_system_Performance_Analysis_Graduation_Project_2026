using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.Profiles;

namespace Trainova.Domain.MedicalStatus
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
        public BodyPart BodyPart { get; private set; }
        public string? Notes { get; private set; }
        public bool IsNew { get; private set; }

        public DateTime? HappendAt { get; private set; }
        public DateTime? ReturnedAt { get; private set; }
        public DateTime? ExpectedReturnDate { get; private set; }

        public List<RecoveryPlanPhase> Phases { get; private set; } = new List<RecoveryPlanPhase>();


        public PlayerInjury(
            Guid playerId,
            Guid injuryId,
            InjuryStatus status,
            DateTime? happendAt = null,
            InjuryCause cause = default,
            SevertiyGrade severtiyGrade = default,
            BodyPart bodyPart = default,
            string notes = null,
            bool isNew = false,
            DateTime? expectedReturnDate = null,
            Guid? createdBy = null) : base(Guid.NewGuid(), createdBy)
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
            BodyPart? bodyPart = null,
            string? notes = null,
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


        public void AddRecoveryPlanPhase(RecoveryPlanPhase phase)
        {
            phase.SetOrder(Phases.Count);
            Phases.Add(phase);
        }
        public void ReorderPhases(List<int> newOrder)
        {
            if (newOrder.Count != Phases.Count)
                throw new DomainException("Invalid reorder list", "InvalidOrder");

            var phaseByOrder = Phases.ToDictionary(p => p.Order);

            var newList = new List<RecoveryPlanPhase>();

            for (int i = 0; i < newOrder.Count; i++)
            {
                var oldOrder = newOrder[i];

                if (!phaseByOrder.TryGetValue(oldOrder, out var phase))
                    throw new DomainException("Phase not found", "PhaseNotFound");

                phase.SetOrder(i);
                newList.Add(phase);
            }

            Phases = newList;
        }
        public void RemovePhase(int order)
        {
            if (Phases.Count() >= order || !Phases.Any())
                throw new DomainException("order is out of the list", "EmptyPhasesOrOrderOutOfRange");

            var phaseToBeDeleted = Phases.FirstOrDefault(p => p.Order == order);

            if (phaseToBeDeleted == null)
                throw new DomainException("No phase with that order", "PhaseNotFound");

            Phases.Remove(phaseToBeDeleted);
        }

        private PlayerInjury() : base() { }

    }
}
