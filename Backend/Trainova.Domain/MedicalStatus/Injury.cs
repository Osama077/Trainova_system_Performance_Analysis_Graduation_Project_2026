using Trainova.Domain.Common.BaseEntity;

namespace Trainova.Domain.MedicalStatus
{
    public class Injury : AuditableEntity<Guid>
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public InjuryType? InjuryType { get; private set; }
        public int? AverageRecoveryTimeInDayes { get; private set; }

        public ICollection<PlayerInjury> PlayerInjuries { get; private set; } = new List<PlayerInjury>();
        private Injury() :base() { }

        public void Update(
            string? name = null,
            string? description = null,
            InjuryType? injuryType = null,
            int? averageRecoveryTime = null)
        {
            MarkUpdatedNow();

            Name = name ?? Name;
            Description = description ?? Description;
            InjuryType = injuryType ?? InjuryType;
            AverageRecoveryTimeInDayes = averageRecoveryTime ?? AverageRecoveryTimeInDayes;
        }

        public Injury(
            string name,
            string? description,
            InjuryType? injuryType = null,
            int? averageRecoveryTime = null,
            Guid? createdBy = null) : base(Guid.NewGuid(), createdBy)
        {
            Name = name;
            Description = description;
            InjuryType = injuryType;
            AverageRecoveryTimeInDayes = averageRecoveryTime;
        }
    }
}
