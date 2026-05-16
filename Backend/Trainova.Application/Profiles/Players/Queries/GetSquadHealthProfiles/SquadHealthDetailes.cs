using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.Profiles.Players.Queries.GetSquadHealthProfiles
{
    public class SquadHealthDetailes
    {
        public IEnumerable<SquadHealthProfilesDataReadingModel> SquadHealthProfiles { get; set; }

        public GeneralStats General { get; set; } = new();
        public SeverityStats Severity { get; set; } = new();
        public StatusStats Status { get; set; } = new();
        public CauseStats Cause { get; set; } = new();
        public TypeStats Type { get; set; } = new();

        public SquadHealthDetailes(IEnumerable<SquadHealthProfilesDataReadingModel> profiles)
        {
            SquadHealthProfiles = profiles ?? new List<SquadHealthProfilesDataReadingModel>();
            General.TotalPlayers = SquadHealthProfiles.Count();

            foreach (var profile in SquadHealthProfiles)
            {
                if (profile.PlayerInjuryId.HasValue && profile.InjuryStatus != nameof(InjuryStatus.Ended))
                {
                    General.TotalInjured++;

                    // 1. New vs Old
                    if (profile.IsNew.HasValue)
                    {
                        if (profile.IsNew.Value) General.TotalIsNew++;
                        else General.TotalIsNotNew++;
                    }

                    // 2. Severity Grade
                    if (profile.SevertiyGrade.HasValue)
                    {
                        switch ((SeverityGrade)profile.SevertiyGrade.Value)
                        {
                            case SeverityGrade.Mild: Severity.Mild++; break;
                            case SeverityGrade.Medium: Severity.Medium++; break;
                            case SeverityGrade.Severe: Severity.Severe++; break;
                        }
                    }

                    // 3. Injury Status
                    if (!string.IsNullOrEmpty(profile.InjuryStatus))
                    {
                        if (profile.InjuryStatus == nameof(InjuryStatus.InHealing)) Status.InHealing++;
                        else if (profile.InjuryStatus == nameof(InjuryStatus.InRecovery)) Status.InRecovery++;
                    }

                    // 4. Injury Cause
                    if (!string.IsNullOrEmpty(profile.Cause))
                    {
                        if (profile.Cause == nameof(InjuryCause.Training)) Cause.Training++;
                        else if (profile.Cause == nameof(InjuryCause.Match)) Cause.Match++;
                        else if (profile.Cause == nameof(InjuryCause.OverUse)) Cause.OverUse++;
                        else if (profile.Cause == nameof(InjuryCause.Collision)) Cause.Collision++;
                        else Cause.Unknown++;
                    }

                    // 5. Injury Type
                    if (!string.IsNullOrEmpty(profile.InjuryType))
                    {
                        if (profile.InjuryType == nameof(InjuryType.Muscular)) Type.Muscular++;
                        else if (profile.InjuryType == nameof(InjuryType.Bone)) Type.Bone++;
                        else if (profile.InjuryType == nameof(InjuryType.Joint)) Type.Joint++;
                        else if (profile.InjuryType == nameof(InjuryType.Ligament)) Type.Ligament++;
                        else Type.Other++;
                    }
                }
                else
                {
                    General.TotalHealthy++;

                    if (profile.InjuryStatus == nameof(InjuryStatus.Ended))
                    {
                        Status.Ended++;
                    }
                }
            }
        }

        // ==========================================
        // Nested Classes
        // ==========================================
        public class GeneralStats
        {
            public int TotalPlayers { get; set; }
            public int TotalInjured { get; set; }
            public int TotalHealthy { get; set; }
            public int TotalIsNew { get; set; }
            public int TotalIsNotNew { get; set; }
        }

        public class SeverityStats
        {
            public int Mild { get; set; }
            public int Medium { get; set; }
            public int Severe { get; set; }
        }

        public class StatusStats
        {
            public int InHealing { get; set; }
            public int InRecovery { get; set; }
            public int Ended { get; set; }
        }

        public class CauseStats
        {
            public int Training { get; set; }
            public int Match { get; set; }
            public int OverUse { get; set; }
            public int Collision { get; set; }
            public int Unknown { get; set; }
        }

        public class TypeStats
        {
            public int Muscular { get; set; }
            public int Bone { get; set; }
            public int Joint { get; set; }
            public int Ligament { get; set; }
            public int Other { get; set; }
        }
    }



    public class SquadHealthProfilesDataReadingModel
    {
        public Guid PlayerId { get; set; }
        public string ShowName { get; set; }
        public string FullName { get; set; }
        public string PhotoPath { get; set; }
        public string Email { get; set; }

        public int PlayerNumber { get; set; }
        public string TShirtName { get; set; }
        public string PlayerMedicalStatus { get; set; }
        public int CurrentMainPosition { get; set; }
        public int? OtherAvailablePositions { get; set; }
        public int? PerformanceLevel { get; set; }
        public DateTime DateOfEnrolment { get; set; }

        public Guid? PlayerInjuryId { get; set; }
        public string InjuryStatus { get; set; }
        public string Cause { get; set; }
        public int? SevertiyGrade { get; set; }
        public string BodyPart { get; set; }
        public string Notes { get; set; }
        public bool? IsNew { get; set; }
        public DateTime? HappendAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }

        public Guid? InjuryId { get; set; }
        public string InjuryName { get; set; }
        public int? AverageRecoveryTimeInDayes { get; set; }
        public string InjuryDescription { get; set; }
        public string? InjuryType { get; set; }

        public decimal ProgressPercentage { get; set; } = 0;
    }

}