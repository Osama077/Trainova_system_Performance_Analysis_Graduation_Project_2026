using System.Text.Json.Serialization;
using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.MedicalStatus.PlayerInjuries;

namespace Trainova.Domain.MedicalStatus.Injuries
{
    public class Injury : IAuditable<Guid>
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public InjuryType? InjuryType { get; private set; }
        public TimeSpan? AverageRecoveryTime { get; private set; }
        public Guid? CreatedBy { get; private set; }
        object IAuditable.Id => Id;

        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        public ICollection<PlayerInjury> PlayerInjuries { get; private set; } = new List<PlayerInjury>();
        private Injury() { }
        [JsonConstructor]
        private Injury(
            Guid id,
            string name,
            string? description,
            InjuryType? injuryType,
            TimeSpan? averageRecoveryTime,
            Guid? createdBy,
            DateTime createdAt,
            DateTime? lastUpdate)
        {
            Id = id;
            Name = name;
            Description = description;
            InjuryType = injuryType;
            AverageRecoveryTime = averageRecoveryTime;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            LastUpdate = lastUpdate;
        }
        public void Update(
            string? name = null,
            string? description = null,
            InjuryType? injuryType = null,
            TimeSpan? averageRecoveryTime = null)
        {
            Name = name ?? Name;
            Description = description ?? Description;
            InjuryType = injuryType ?? InjuryType;
            AverageRecoveryTime = averageRecoveryTime ?? AverageRecoveryTime;
            LastUpdate = DateTime.UtcNow;
        }

        public Injury(
            string name,
            string? description,
            InjuryType? injuryType = null,
            TimeSpan? averageRecoveryTime = null,
            Guid? createdBy = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            InjuryType = injuryType;
            AverageRecoveryTime = averageRecoveryTime;
            CreatedBy = createdBy;
            CreatedAt = DateTime.UtcNow;
            LastUpdate = null;
        }
    }
}
