using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.Common.Helpers;

namespace Trainova.Domain.Scouting
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
        public string? Nationality { get; private set; }
        public DateTime? ContractEnd { get; private set; }
        public decimal? MarketValue { get; private set; }
        public string? Agent { get; private set; }
        public float ScoutRating { get; private set; }
        public int? ShortlistRank { get; private set; }
        public int MatchesWatchedCount { get; private set; }
        // Skills (0-100)
        public int Pace { get; private set; }
        public int Shooting { get; private set; }
        public int Dribbling { get; private set; }
        public int Passing { get; private set; }
        public int Physicality { get; private set; }
        public int Positioning { get; private set; }
        public int Defending { get; private set; }
        public int Vision { get; private set; }
        // Free-form scout notes / summary (stored on candidate)
        public string? Notes { get; private set; }
        // Candidate status flags (shortlisted, on-trial, watched, rejected, etc.)
        public CandidateStatus Status { get; private set; } = CandidateStatus.None;
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
            string? nationality = null,
            DateTime? contractEnd = null,
            decimal? marketValue = null,
            string? agent = null,
            float scoutRating = 0,
            int? shortlistRank = null,
            int matchesWatchedCount = 0,
            int pace = 0,
            int shooting = 0,
            int dribbling = 0,
            int passing = 0,
            int physicality = 0,
            int positioning = 0,
            int defending = 0,
            int vision = 0,
            string? notes = null,
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
            Nationality = nationality;
            ContractEnd = contractEnd;
            MarketValue = marketValue;
            Agent = agent;
            ScoutRating = scoutRating;
            ShortlistRank = shortlistRank;
            MatchesWatchedCount = matchesWatchedCount;
            // skills
            Pace = pace;
            Shooting = shooting;
            Dribbling = dribbling;
            Passing = passing;
            Physicality = physicality;
            Positioning = positioning;
            Defending = defending;
            Vision = vision;
            Notes = notes;
        }
        /// <summary>
        /// Add one or more status flags to the candidate.
        /// </summary>
        public void AddStatus(CandidateStatus flags)
        {
            if (flags == CandidateStatus.None) return;
            Status |= flags;
            MarkUpdatedNow();
        }

        /// <summary>
        /// Remove one or more status flags from the candidate.
        /// </summary>
        public void RemoveStatus(CandidateStatus flags)
        {
            if (flags == CandidateStatus.None) return;
            Status &= ~flags;
            MarkUpdatedNow();
        }

        /// <summary>
        /// Checks whether candidate has the specified status flag(s) (any).
        /// </summary>
        public bool HasStatus(CandidateStatus flags)
        {
            if (flags == CandidateStatus.None) return Status == CandidateStatus.None;
            return (Status & flags) != 0;
        }
        public void Update(
            string? fullName= null,
            int? age= null,
            PlayerMedicalStatus? medecalStatus = null,
            Position? currentMainPosition = null,
            Position? otherAvailablePositions = null,
            decimal? performanceLevel = null,
            string? nationality = null,
            DateTime? contractEnd = null,
            decimal? marketValue = null,
            string? agent = null,
            float? scoutRating = null,
            int? shortlistRank = null,
            int? matchesWatchedCount = null,
            int? pace = null,
            int? shooting = null,
            int? dribbling = null,
            int? passing = null,
            int? physicality = null,
            int? positioning = null,
            int? defending = null,
            int? vision = null,
            string? notes = null)
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
            Nationality = nationality ?? Nationality;
            ContractEnd = contractEnd ?? ContractEnd;
            MarketValue = marketValue ?? MarketValue;
            Agent = agent ?? Agent;
            ScoutRating = scoutRating ?? ScoutRating;
            ShortlistRank = shortlistRank ?? ShortlistRank;
            MatchesWatchedCount = matchesWatchedCount ?? MatchesWatchedCount;
            Pace = pace ?? Pace;
            Shooting = shooting ?? Shooting;
            Dribbling = dribbling ?? Dribbling;
            Passing = passing ?? Passing;
            Physicality = physicality ?? Physicality;
            Positioning = positioning ?? Positioning;
            Defending = defending ?? Defending;
            Vision = vision ?? Vision;
            Notes = notes ?? Notes;
        }

    }
}
