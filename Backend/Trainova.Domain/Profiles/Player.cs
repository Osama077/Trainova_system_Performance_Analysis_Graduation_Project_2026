using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.FitnessStatus;
using Trainova.Domain.MedicalStatus;
using Trainova.Domain.UserAuth;

namespace Trainova.Domain.Profiles
{
    public class Player : AuditableEntity<Guid>
    {
        public User User { get; private set; }

        public int PlayerNumber { get; private set; }
        public string TShirtName { get; private set; }
        public PlayerMedicalStatus MedicalStatus { get; private set; } = PlayerMedicalStatus.Fit;
        public Position CurrentMainPosition { get; private set; }
        public Position OtherAvailablePositions { get; private set; }
        public decimal PerformanceLevel { get; private set; }
        public DateOnly DateOfEnrolment { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public ICollection<PlayerInjury> PlayerInjuries { get; private set; } = [];


        private Player() : base()
        {
        }

        public Player(
            Guid id,
            int playerNumber,
            string tShirtName,
            PlayerMedicalStatus medecalStatus,
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

            PlayerNumber = playerNumber;
            TShirtName = tShirtName;
            MedicalStatus = medecalStatus;
            CurrentMainPosition = currentMainPosition;
            OtherAvailablePositions = otherAvailablePositions;
            PerformanceLevel = performanceLevel;
            DateOfEnrolment = dateOfEnrolment;
        }

        public void Update(
            int? playerNumber = null,
            string? tShirtName = null,
            PlayerMedicalStatus? medecalStatus = null,
            Position? currentMainPosition = null,
            Position? otherAvailablePositions = null,
            decimal? performanceLevel = null)
        {
            MarkUpdatedNow();

            if (currentMainPosition.HasValue)
            {
                if (!currentMainPosition.Value.HasSingleFlag())
                    throw new DomainException(
                        "Player must have exactly one main position.",
                        "DomainError_MainPositionDontFit");
            }

            PlayerNumber = playerNumber ?? PlayerNumber;
            TShirtName = tShirtName ?? TShirtName;
            MedicalStatus = medecalStatus ?? MedicalStatus;
            CurrentMainPosition = currentMainPosition ?? CurrentMainPosition;
            OtherAvailablePositions = otherAvailablePositions ?? OtherAvailablePositions;
            PerformanceLevel = performanceLevel ?? PerformanceLevel;
        }

        public void MarkAsInjuried()
        {
            MarkUpdatedNow();
            MedicalStatus = PlayerMedicalStatus.Injured;
        }
        public void TriggereMedicalStatusChange(PlayerMedicalStatus newStatus)
        {
            if (MedicalStatus == newStatus)
                throw new DomainException("Medical status is already set to the specified value.", "DomainError_MedicalStatusUnchanged");
            MarkUpdatedNow();
            MedicalStatus = newStatus;
        }

        public PhysicalCapacityTest GenreteNewPhysicalTest(
            ExplosivePowerTest explosivePowerTest,
            AerobicCapacityTest aerobicCapacityTest,
            SprintTest sprintTest,
            PhysicalCapacityTest? lastPhysicalCapacityTest = null,
            CreationType creationType = CreationType.Manual,
            Guid? createdBy = null)
        {
            return new PhysicalCapacityTest(
                playerId: Id,
                aerobicCapacityTest: aerobicCapacityTest,
                sprintTest: sprintTest,
                explosivePowerTest: explosivePowerTest,
                lastPhysicalCapacityTest: lastPhysicalCapacityTest,
                creationType: creationType,
                createdBy: createdBy);
        }


    }
}
