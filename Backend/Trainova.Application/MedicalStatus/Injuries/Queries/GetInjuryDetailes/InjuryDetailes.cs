using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.MedicalStatus.Injuries.Queries.GetInjuryDetailes
{
    public class InjuryDetailes
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public InjuryType Type { get; set; }
        public int AverageRecoveryTimeInDayes { get; set; }
        public int PlayerInjuriesCount { get; set; }
        public int PlayeresInjuredCount { get; set; } = 0;
        public int CurrentlyInHealingCount { get; set; }

    }
}
