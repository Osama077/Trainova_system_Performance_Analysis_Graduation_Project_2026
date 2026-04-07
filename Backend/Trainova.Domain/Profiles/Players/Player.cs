using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.MatchsManagement.ComputedFeatures;
using Trainova.Domain.MatchsManagement.Events;
using Trainova.Domain.MatchsManagement.Lineups;
using Trainova.Domain.MedicalStatus.PlayerInjuries;
using Trainova.Domain.SeasonsAnalyses.ModelScores;
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
        public DateOnly DateOfEnrolment { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public ICollection<Lineup> Lineups { get; private set; } = [];
        public ICollection<ModelScore> ModelScores {  get; private set; } = [];
        public ICollection<PlayerInjury> PlayerInjuries { get; private set; } = [];
        public ICollection<ComputedFeature> ComputedFeatures { get; private set; } = [];
        public ICollection<Event> Events { get; private set; } = [];



        private Player() : base()
        {
        }

        public Player(
            Guid id,
            Guid currentTeamId,
            int playerNumber,
            string tShirtName,
            PlayerMedecalStatus medecalStatus,
            Position currentMainPosition,
            Position otherAvailablePositions,
            decimal performanceLevel,
            DateOnly dateOfEnrolment,
            Guid? createdBy = null)
            : base(id, createdBy)
        {
            if (!currentMainPosition.HasSingleFlag())
                throw new DomainException(
                    "Player must have exactly one main position.",
                    "DomainError_MainPositionDontFit");

            CurrentTeamId = currentTeamId;
            PlayerNumber = playerNumber;
            TShirtName = tShirtName;
            MedecalStatus = medecalStatus;
            CurrentMainPosition = currentMainPosition;
            OtherAvailablePositions = otherAvailablePositions;
            PerformanceLevel = performanceLevel;
            DateOfEnrolment = dateOfEnrolment;
        }

        public void Update(
            int? playerNumber = null,
            string? tShirtName = null,
            PlayerMedecalStatus? medecalStatus = null,
            Position? currentMainPosition = null,
            Position? otherAvailablePositions = null,
            decimal? performanceLevel = null)
        {
            if (currentMainPosition.HasValue)
            {
                if (!currentMainPosition.Value.HasSingleFlag())
                    throw new DomainException(
                        "Player must have exactly one main position.",
                        "DomainError_MainPositionDontFit");
            }

            PlayerNumber = playerNumber?? PlayerNumber;
            TShirtName = tShirtName ?? TShirtName;
            MedecalStatus = medecalStatus?? MedecalStatus;
            CurrentMainPosition = currentMainPosition ?? CurrentMainPosition;
            OtherAvailablePositions = otherAvailablePositions ?? OtherAvailablePositions;
            PerformanceLevel = performanceLevel ?? PerformanceLevel;
            MarkUpdatedNow();
        }


    }
}
