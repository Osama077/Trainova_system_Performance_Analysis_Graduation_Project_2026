using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.Scouting.ValueObjects;

namespace Trainova.Domain.Scouting
{
    public class ScoutingCandidate : AuditableEntity<Guid>
    {
        public string FullName { get; private set; } = string.Empty;
        public int Age { get; private set; }
        public Position Position { get; private set; }
        // public float PerformanceScore { get; private set; }
        // public float InjuryRisk { get; private set; }
        // public PlayerMedicalStatus MedecalStatus { get; private set; } = PlayerMedicalStatus.Fit;
        // public Position CurrentMainPosition { get; private set; }
        // public Position OtherAvailablePositions { get; private set; }
        // public decimal PerformanceLevel { get; private set; }
        public string? CurrentTeamName { get; private set; }
        public float ScoutRating { get; private set; }
        public int? ShortlistRank { get; private set; }
        public int MatchesWatchedCount { get; private set; }
        // Free-form scout notes / summary (stored on candidate)
        public string? Notes { get; private set; }
        // Candidate status flags (shortlisted, on-trial, watched, rejected, etc.)
        public CandidateStatus Status { get; private set; } = CandidateStatus.None;

        // Value Objects
        public PersonalDetails PersonalDetails { get; private set; } = null!;
        public SkillAssessment SkillAssessment { get; private set; } = null!;
        public ContractInfo ContractInfo { get; private set; } = null!;
        private ScoutingCandidate() : base() { }
        public ScoutingCandidate(
            string fullName,
            int age,
            Position position,
            // float performanceScore,
            // float injuryRisk,
            // PlayerMedicalStatus medecalStatus,
            // Position currentMainPosition,
            // Position otherAvailablePositions,
            // decimal performanceLevel,
            string? currentTeamName,
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
            DateTime? dateOfBirth = null,
            int? height = null,
            int? weight = null,
            string preferredFoot = "Right",
            Guid? createdBy = null)
            : base(Guid.NewGuid(), createdBy)
        {
            // if (!currentMainPosition.HasSingleFlag())
            //     throw new DomainException(
            //         "Player must have exactly one main position.",
            //         "DomainError_MainPositionDontFit");
            FullName = fullName;
            Age = age;
            Position = position;
            // PerformanceScore = performanceScore;
            // InjuryRisk = injuryRisk;
            // MedecalStatus = medecalStatus;
            // CurrentMainPosition = currentMainPosition;
            // OtherAvailablePositions = otherAvailablePositions;
            // PerformanceLevel = performanceLevel;
            CurrentTeamName = currentTeamName;
            ScoutRating = scoutRating;
            ShortlistRank = shortlistRank;
            MatchesWatchedCount = matchesWatchedCount;
            Notes = notes;
            
            // Initialize Value Objects
            PersonalDetails = new PersonalDetails(dateOfBirth, height, weight, preferredFoot);
            SkillAssessment = new SkillAssessment(pace, shooting, dribbling, passing, physicality, positioning, defending, vision);
            ContractInfo = new ContractInfo(nationality, contractEnd, marketValue, agent);
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
        /// Overwrite the candidate's status with the given value (replace all flags).
        /// </summary>
        public void SetStatus(CandidateStatus status)
        {
            Status = status;
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
            string? currentTeamName = null,
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
            string? notes = null,
            DateTime? dateOfBirth = null,
            int? height = null,
            int? weight = null,
            string? preferredFoot = null)
        {
            MarkUpdatedNow();

            FullName = fullName?? FullName;
            Age = age?? Age;
            CurrentTeamName = currentTeamName ?? CurrentTeamName;
            ScoutRating = scoutRating ?? ScoutRating;
            ShortlistRank = shortlistRank ?? ShortlistRank;
            MatchesWatchedCount = matchesWatchedCount ?? MatchesWatchedCount;
            Notes = notes ?? Notes;

            // Update Value Objects
            ContractInfo.Update(nationality, contractEnd, marketValue, agent);
            SkillAssessment.Update(pace, shooting, dribbling, passing, physicality, positioning, defending, vision);
            PersonalDetails.Update(dateOfBirth, height, weight, preferredFoot);
        }

        public ICollection<ScoutingCandidateNote> NotesList { get; private set; } = new List<ScoutingCandidateNote>();

        public ICollection<CandidateMatch> MatchesList { get; private set; } = new List<CandidateMatch>();

        public ICollection<SeasonStatistics> SeasonsList { get; private set; } = new List<SeasonStatistics>();

        public Guid AddNote(string text, Guid? createdBy, string? createdByName = null)
        {
            if (string.IsNullOrWhiteSpace(text)) return Guid.Empty;
            var note = new ScoutingCandidateNote(this.Id, text, createdBy, createdByName);
            NotesList.Add(note);
            // keep Notes snippet for legacy UI
            Notes = text;
            MarkUpdatedNow();
            return note.Id;
        }
        public bool RemoveNote(Guid noteId)
        {
            var note = NotesList.FirstOrDefault(n => n.Id == noteId);
            if (note == null) return false;
            NotesList.Remove(note);
            // update snippet if removed note was the last stored snippet
            if (Notes == note.Text)
                Notes = NotesList.OrderByDescending(n => n.CreatedAt).FirstOrDefault()?.Text;
            MarkUpdatedNow();
            return true;
        }

        public Guid AddMatch(DateTime matchDate, string matchName, int goals, int assists, float rating, string? scoutNotes)
        {
            if (string.IsNullOrWhiteSpace(matchName)) return Guid.Empty;
            var match = new CandidateMatch(this.Id, matchDate, matchName, goals, assists, rating, scoutNotes);
            MatchesList.Add(match);
            MatchesWatchedCount = MatchesList.Count;
            MarkUpdatedNow();
            return match.Id;
        }

        public bool RemoveMatch(Guid matchId)
        {
            var match = MatchesList.FirstOrDefault(m => m.Id == matchId);
            if (match == null) return false;
            MatchesList.Remove(match);
            MatchesWatchedCount = MatchesList.Count;
            MarkUpdatedNow();
            return true;
        }

        public Guid AddSeason(string season, string league, int goals, int assists, int matches, float passAccuracy, float shotsPer90, float xgPer90)
        {
            if (string.IsNullOrWhiteSpace(season)) return Guid.Empty;
            var seasonStats = new SeasonStatistics(this.Id, season, league, goals, assists, matches, passAccuracy, shotsPer90, xgPer90);
            SeasonsList.Add(seasonStats);
            MarkUpdatedNow();
            return seasonStats.Id;
        }

        public bool RemoveSeason(Guid seasonId)
        {
            var season = SeasonsList.FirstOrDefault(s => s.Id == seasonId);
            if (season == null) return false;
            SeasonsList.Remove(season);
            MarkUpdatedNow();
            return true;
        }

    }
}
