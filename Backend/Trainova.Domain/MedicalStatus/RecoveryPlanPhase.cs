using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Domain.MedicalStatus
{
    public class RecoveryPlanPhase : AuditableEntity<Guid>
    {
        public Guid PlayerInjuryId { get; private set; }
        public string Name { get; private set; }
        public int Order { get; private set; } = 0;
        public string? Description { get; private set; }
        public DateTime From { get; private set; } = DateTime.UtcNow;
        public DateTime To { get; private set; }
        public List<string> Activities { get; private set; } = new List<string>();
        public RecoveryPlanPhase(
            Guid playerInjuryId,
            string name,
            string? description,
            DateTime to,
            DateTime? from = null,
            List<string> activties = null,
            Guid? createdBy = null)
            : base(Guid.NewGuid(), createdBy)
        {
            PlayerInjuryId = playerInjuryId;
            Name = name;
            Description = description;
            From = from ?? DateTime.UtcNow;
            To = to;
            Activities = new List<string>();
            if (activties != null)
                Activities.AddRange(activties);
        }
        public void SetOrder(int order)
        {
            MarkUpdatedNow();
            Order = order;
        }
        public RecoveryPlanPhase Update(
            string? name = null,
            string? description = null,
            DateTime? to = null,
            List<string>? activties = null)
        {
            MarkUpdatedNow();
            Name = name ?? Name;
            Description = description ?? Description;
            To = to ?? To;
            if (activties != null)
            {
                Activities.Clear();
                Activities.AddRange(activties);
            }
            return this;
        }
        public TimeSpan Duration => To - From;

        public void UpdateForReOrder(int newOrder, DateTime newStartDate, DateTime newEndDate)
        {
            if (newEndDate < newStartDate)
                throw new DomainException("End date cannot be before start date", "InvalidPhaseDates");

            MarkUpdatedNow();
            Order = newOrder;
            From = newStartDate;
            To = newEndDate;
        }

        private RecoveryPlanPhase() { }
    }
}
