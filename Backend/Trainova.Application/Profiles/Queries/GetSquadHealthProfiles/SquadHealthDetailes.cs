using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.Profiles.Queries.GetSquadHealthProfiles
{
    public class SquadHealthDetailes
    {
        public IEnumerable<PlyersForPosition> SquadHealthProfiles { get; set; }

        public GeneralStats General { get; set; } = new();
        public SeverityStats Severity { get; set; } = new();
        public StatusStats Status { get; set; } = new();
        public CauseStats Cause { get; set; } = new();
        public TypeStats Type { get; set; } = new();

        public SquadHealthDetailes(IEnumerable<SquadHealthProfilesDataReadingModel> flatProfiles)
        {
            var rawProfiles = flatProfiles ?? new List<SquadHealthProfilesDataReadingModel>();

            General.TotalPlayers = rawProfiles.Select(p => p.PlayerId).Distinct().Count();

            var groupedPlayers = rawProfiles
                .GroupBy(p => p.PlayerId)
                .Select(group =>
                {
                    var firstRow = group.First();

                    var injuriesList = group
                        .Where(i => i.PlayerInjuryId.HasValue && i.InjuryStatus != nameof(InjuryStatus.Ended))
                        .Select(i => new PlayerInjuryDetailModel(
                            i.PlayerInjuryId!.Value,
                            i.InjuryStatus,
                            i.Cause,
                            i.SevertiyGrade,
                            i.BodyPart,
                            i.Notes,
                            i.IsNew,
                            i.HappendAt,
                            i.ReturnedAt,
                            i.ExpectedReturnDate,
                            i.InjuryId,
                            i.InjuryName,
                            i.AverageRecoveryTimeInDayes,
                            i.InjuryDescription,
                            i.InjuryType,
                            i.ProgressPercentage
                        )).ToList();

                    return new PlayerHealthProfileModel(
                        firstRow.PlayerId,
                        firstRow.ShowName,
                        firstRow.FullName,
                        firstRow.PhotoPath,
                        firstRow.Email,
                        firstRow.PlayerNumber,
                        firstRow.TShirtName,
                        firstRow.PlayerMedicalStatus,
                        firstRow.CurrentMainPosition,
                        firstRow.OtherAvailablePositions,
                        firstRow.PerformanceLevel,
                        firstRow.DateOfEnrolment,
                        injuriesList
                    );
                }).ToList();

            foreach (var player in groupedPlayers)
            {
                if (player.Injuries.Any())
                {
                    General.TotalInjured++;

                    foreach (var injury in player.Injuries)
                    {
                        if (injury.IsNew.HasValue)
                        {
                            if (injury.IsNew.Value) General.TotalIsNew++;
                            else General.TotalIsNotNew++;
                        }

                        if (injury.SevertiyGrade.HasValue)
                        {
                            switch ((SeverityGrade)injury.SevertiyGrade.Value)
                            {
                                case SeverityGrade.Mild: Severity.Minor++; break;
                                case SeverityGrade.Medium: Severity.Moderate++; break;
                                case SeverityGrade.Severe: Severity.Severe++; break;
                            }
                        }

                        if (!string.IsNullOrEmpty(injury.InjuryStatus))
                        {
                            if (injury.InjuryStatus == nameof(InjuryStatus.InHealing)) Status.InHealing++;
                            else if (injury.InjuryStatus == nameof(InjuryStatus.InRecovery)) Status.InRecovery++;
                        }

                        if (!string.IsNullOrEmpty(injury.Cause))
                        {
                            if (injury.Cause == nameof(InjuryCause.Training)) Cause.Training++;
                            else if (injury.Cause == nameof(InjuryCause.Match)) Cause.Match++;
                            else if (injury.Cause == nameof(InjuryCause.OverUse)) Cause.OverUse++;
                            else if (injury.Cause == nameof(InjuryCause.Collision)) Cause.Collision++;
                            else Cause.Unknown++;
                        }

                        if (!string.IsNullOrEmpty(injury.InjuryType))
                        {
                            if (injury.InjuryType == nameof(InjuryType.Muscular)) Type.Muscular++;
                            else if (injury.InjuryType == nameof(InjuryType.Bone)) Type.Bone++;
                            else if (injury.InjuryType == nameof(InjuryType.Joint)) Type.Joint++;
                            else if (injury.InjuryType == nameof(InjuryType.Ligament)) Type.Ligament++;
                            else Type.Other++;
                        }
                    }
                }
                else
                {
                    General.TotalHealthy++;
                }
            }

            SquadHealthProfiles = groupedPlayers
                .GroupBy(p => p.CurrentMainPosition)
                .Select(posGroup => new PlyersForPosition(
                    posGroup.Key,
                    posGroup.ToList(),
                    posGroup.Count()
                )).ToList();
        }

        public record GeneralStats
        {
            public int TotalPlayers { get; set; }
            public int TotalInjured { get; set; }
            public int TotalHealthy { get; set; }
            public int TotalIsNew { get; set; }
            public int TotalIsNotNew { get; set; }
        }

        public record SeverityStats
        {
            public int Minor { get; set; }
            public int Moderate { get; set; }
            public int Severe { get; set; }
        }

        public record StatusStats
        {
            public int InHealing { get; set; }
            public int InRecovery { get; set; }
            public int Ended { get; set; }
        }

        public record CauseStats
        {
            public int Training { get; set; }
            public int Match { get; set; }
            public int OverUse { get; set; }
            public int Collision { get; set; }
            public int Unknown { get; set; }
        }

        public record TypeStats
        {
            public int Muscular { get; set; }
            public int Bone { get; set; }
            public int Joint { get; set; }
            public int Ligament { get; set; }
            public int Other { get; set; }
        }
    }

    public record PlyersForPosition(int Position, List<PlayerHealthProfileModel> Players, int Count);

    public record PlayerHealthProfileModel(
        Guid PlayerId,
        string ShowName,
        string FullName,
        string PhotoPath,
        string Email,
        int PlayerNumber,
        string TShirtName,
        string PlayerMedicalStatus,
        int CurrentMainPosition,
        int? OtherAvailablePositions,
        int? PerformanceLevel,
        DateTime DateOfEnrolment,
        List<PlayerInjuryDetailModel> Injuries
    );

    public record PlayerInjuryDetailModel(
        Guid PlayerInjuryId,
        string InjuryStatus,
        string Cause,
        int? SevertiyGrade,
        string BodyPart,
        string Notes,
        bool? IsNew,
        DateTime? HappendAt,
        DateTime? ReturnedAt,
        DateTime? ExpectedReturnDate,
        Guid? InjuryId,
        string InjuryName,
        int? AverageRecoveryTimeInDayes,
        string InjuryDescription,
        string InjuryType,
        decimal ProgressPercentage
    );

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