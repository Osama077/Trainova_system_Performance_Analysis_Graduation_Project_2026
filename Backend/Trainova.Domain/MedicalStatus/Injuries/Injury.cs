using System.Text.Json.Serialization;
using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.MedicalStatus.PlayerInjuries;

namespace Trainova.Domain.MedicalStatus.Injuries
{
    public class Injury : AuditableEntity<Guid>
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public InjuryType? InjuryType { get; private set; }
        public TimeSpan? AverageRecoveryTime { get; private set; }

        public ICollection<PlayerInjury> PlayerInjuries { get; private set; } = new List<PlayerInjury>();
        private Injury() :base() { }

        public void Update(
            string? name = null,
            string? description = null,
            InjuryType? injuryType = null,
            TimeSpan? averageRecoveryTime = null)
        {
            MarkUpdatedNow();

            Name = name ?? Name;
            Description = description ?? Description;
            InjuryType = injuryType ?? InjuryType;
            AverageRecoveryTime = averageRecoveryTime ?? AverageRecoveryTime;
        }

        public Injury(
            string name,
            string? description,
            InjuryType? injuryType = null,
            TimeSpan? averageRecoveryTime = null,
            Guid? createdBy = null) : base(Guid.NewGuid(), createdBy)
        {
            Name = name;
            Description = description;
            InjuryType = injuryType;
            AverageRecoveryTime = averageRecoveryTime;
        }
    }
}
