using Trainova.Application.Common.Interfaces.MarkUps;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetPlayerInjuries
{
    // Read model for joined PlayerInjury + Player + Injury view
    public class PlayerInjuryReadModel:ITotalCountIncluded
    {
        public Guid PlayerInjuryId { get; set; }
        public DateTime PlayerInjuryCreatedAt { get; set; }
        public Guid? PlayerInjuryCreatedBy { get; set; }

        public Guid PlayerId { get; set; }
        public int PlayerNumber { get; set; }
        public string? TShirtName { get; set; }
        public string? PlayerMedicalStatus { get; set; }
        public DateTime? DateOfEnrolment { get; set; }

        public string? UserShowName { get; set; }
        public string? UserFullName { get; set; }
        public string? UserEmail { get; set; }

        public Guid InjuryId { get; set; }
        public string? InjuryName { get; set; }
        public string? InjuryDescription { get; set; }
        public string? InjuryType { get; set; }
        public TimeSpan? InjuryAverageRecoveryTime { get; set; }

        public string? Status { get; set; }
        public string? SevertiyGrade { get; set; }
        public string? Cause { get; set; }
        public string? BodyPart { get; set; }
        public string? Notes { get; set; }
        public DateTime? HappendAt { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public bool IsNew { get; set; }

        // Parameterless constructor needed for EF/Dapper mapping
        public PlayerInjuryReadModel() { }
        private int TotalCount { get; set; }

        int ITotalCountIncluded.TotalCount => TotalCount;

    }
}
