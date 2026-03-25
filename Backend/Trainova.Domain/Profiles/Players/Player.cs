using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.SeasonsAnalyses.Teams;
using Trainova.Domain.UserAuth.Users;

namespace Trainova.Domain.Profiles.Players
{
    public class Player : AuditableEntity<Guid>
    {
        public User User { get; private set; }
        public Guid CurrentTeamId { get; private set; }
        public Team CurrentTeam { get; private set; }
        public int PlayerNumber { get; private set; }
        public string TShirtName { get; private set; }
        public PlayerMedecalStatus MedecalStatus { get; private set; } = PlayerMedecalStatus.Fit;
        public Position CurrentMainPosition { get; private set; }
        public Position OtherAvailablePositions { get; private set; }
        public decimal PerformanceLevel { get; private set; }

        public DateOnly DateOfEnrolment { get; private set; }


        private Player() : base()
        {
        }

        public Player(
            Guid currentTeamId,
            int playerNumber,
            string tShirtName,
            PlayerMedecalStatus medecalStatus,
            Position currentMainPosition,
            Position otherAvailablePositions,
            decimal performanceLevel,
            DateOnly dateOfEnrolment)
        {
            CurrentTeamId = currentTeamId;
            PlayerNumber = playerNumber;
            TShirtName = tShirtName;
            MedecalStatus = medecalStatus;
            CurrentMainPosition = currentMainPosition;
            OtherAvailablePositions = otherAvailablePositions;
            PerformanceLevel = performanceLevel;
            DateOfEnrolment = dateOfEnrolment;
        }
    }
}
