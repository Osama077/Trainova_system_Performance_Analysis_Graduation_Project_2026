using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.SeasonsAnalyses.Teams;
using Trainova.Domain.UserAuth.Users;

namespace Trainova.Domain.Profiles.Players
{
    public class Player : IAuditable<Guid>
    {
        public Guid Id { get; private set; }
        public User User { get; private set; }
        public Guid CurrentTeamId { get; private set; }
        public Team CurrentTeam { get; private set; }
        public int PlayerNumber { get; private set; }
        public string TShirtName { get; private set; }
        public PlayerMedecalStatus MedecalStatus { get; private set; } = PlayerMedecalStatus.Fit;
        public Position CurrentMainPosition { get; private set; }
        public Position OtherAvailablePositions { get; private set; }
        public float PerformanceLevel { get; private set; }

        public DateOnly DateOfEnrolment { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        public Guid? CreatedBy { get; private set; }
        object IAuditable.Id => Id;

        private Player() { }

    }
}
