using Trainova.Domain.Common;
using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.SeasonsAnalyses.Teams;

namespace Trainova.Domain.Profiles.ScoutingCandidates
{
    public class ScoutingCandidate : AuditableEntity<Guid>
    {
        public string FullName { get; private set; } = string.Empty;
        public int Age { get; private set; }
        public Position Position { get; private set; }
        public float PerformanceScore { get; private set; }
        public float InjuryRisk { get; private set; }
        public PlayerMedicalStatus MedecalStatus { get; private set; } = PlayerMedicalStatus.Fit;
        public Position CurrentMainPosition { get; private set; }
        public Position OtherAvailablePositions { get; private set; }
        public decimal PerformanceLevel { get; private set; }
        public Guid? CurrentTeamId { get; private set; }
        private ScoutingCandidate() : base() { }
        public ScoutingCandidate(
            string fullName,
            int age,
            Position position,
            float performanceScore,
            float injuryRisk,
            PlayerMedicalStatus medecalStatus,
            Position currentMainPosition,
            Position otherAvailablePositions,
            decimal performanceLevel,
            Guid? currentTeamId,
            Guid? createdBy = null)
            : base(Guid.NewGuid(), createdBy)
        {
            if (!currentMainPosition.HasSingleFlag())
                throw new DomainException(
                    "Player must have exactly one main position.",
                    "DomainError_MainPositionDontFit");
            FullName = fullName;
            Age = age;
            Position = position;
            PerformanceScore = performanceScore;
            InjuryRisk = injuryRisk;
            MedecalStatus = medecalStatus;
            CurrentMainPosition = currentMainPosition;
            OtherAvailablePositions = otherAvailablePositions;
            PerformanceLevel = performanceLevel;
            CurrentTeamId = currentTeamId;
        }
        public void Update(
            string? fullName= null,
            int? age= null,
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

            FullName = fullName?? FullName;
            Age = age?? Age;
            MedecalStatus = medecalStatus ?? MedecalStatus;
            CurrentMainPosition = currentMainPosition ?? CurrentMainPosition;
            OtherAvailablePositions = otherAvailablePositions ?? OtherAvailablePositions;
            PerformanceLevel = performanceLevel ?? PerformanceLevel;
        }

    }
}
