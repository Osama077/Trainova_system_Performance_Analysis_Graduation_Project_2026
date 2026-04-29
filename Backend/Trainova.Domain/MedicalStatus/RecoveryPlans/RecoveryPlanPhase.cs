using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.MedicalStatus.PlayerInjuries;
using Trainova.Domain.Profiles.Players;

namespace Trainova.Domain.MedicalStatus.RecoveryPlans
{
    public class RecoveryPlanPhase : AuditableEntity<Guid>
    {
        public Guid PlayerInjuryId { get; private set; }
        public PlayerInjury PlayerInjury { get; private set; }
        public string Name { get; private set; }
        public byte Order { get; private set; } = 0;
        public string? Description { get; private set; }
        public DateTime From { get; private set; } = DateTime.UtcNow;
        public DateTime? To { get; private set; }
        public List<string> Activities { get; private set; }
        public RecoveryPlanPhase(
            Guid playerInjuryId,
            string name,
            string? description,
            DateTime? from = null,
            DateTime? to = null,
            List<string> activties = null,
            Guid? createdBy = null)
            : base(Guid.NewGuid(), createdBy)
        {
            PlayerInjuryId = playerInjuryId;
            Name = name;
            Description = description;
            From = from.HasValue ? DateTime.UtcNow : DateTime.UtcNow;
            To = to;
            Activities = new List<string>();
            if (activties != null)
                Activities.AddRange(activties);
        }


        public void Update(
            string? name = null,
            string? description = null,
            DateTime? from = null,
            DateTime? to = null,
            List<string>? activties = null)
        {
            MarkUpdatedNow();
            Name = name ?? Name;
            Description = description ?? Description;
            From = from.HasValue ? DateTime.UtcNow : From;
            To = to;
            if (activties != null)
                Activities = activties;
        }
        private RecoveryPlanPhase() : base() { }
    }
}
