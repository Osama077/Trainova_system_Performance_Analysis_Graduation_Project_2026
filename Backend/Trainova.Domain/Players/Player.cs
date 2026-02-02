using Trainova.Domain.Common;
using Trainova.Domain.Users;

namespace Trainova.Domain.Players
{
    public class Player :IAuditable
    {
        public Guid Id { get; private set; }
        public User User { get; private set; }
        public int PlayerNumber { get; private set; }
        public string TShirtName { get; private set; }
        public MedecalStatus MedecalStatus { get; private set; }
        public Position CurrentPosition { get; private set; }
        public float PerformanceScore { get; private set; }
        public float InjuryRisk { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        public Guid? Owner { get; private set; }

        private Player() { }

    }
}
