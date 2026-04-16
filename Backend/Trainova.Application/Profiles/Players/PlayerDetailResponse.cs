using Trainova.Application.Common.Interfaces.MarkUps;

namespace Trainova.Application.Profiles.Players
{
    public class PlayerDetailResponse : ITotalCountIncluded
    {
        public Guid Id { get; set; }
        public string ShowName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string PhotoPath { get; set; }
        public Guid TeamId { get; set; }
        public string TShirtName { get; set; }
        public int PlayerNumber { get; set; }
        public int PerformanceLevel { get; set; }
        public int CurrentMainPosition { get; set; }
        public int OtherAvailablePositions { get; set; }
        public string MedicalStatus { get; set; }
        public DateTime DateOfEnrolment { get; set; }
        public DateTime CreatedAt { get; set; }
        public int MatchesCount { get; set; }
        public int InjuriesCount { get; set; }

        // Captured from COUNT(*) OVER()
        private int TotalCount { get; set; }
        public PlayerDetailResponse() { }
        int ITotalCountIncluded.TotalCount => TotalCount;
    }

    /*
    public class PlayerDetailResponse : ITotalCountincluded
    {
        public Guid Id { get; set; }
        public string ShowName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string PhotoPath { get; set; }
        public Guid TeamId { get; set; }
        public string TShirtName { get; set; }
        public int PlayerNumber { get; set; }
        public int PerformanceLevel { get; set; }
        public int CurrentMainPosition { get; set; }
        public int OtherAvailablePositions { get; set; }
        public string MedicalStatus { get; set; }
        public DateTime DateOfEnrolment { get; set; }
        public DateTime CreatedAt { get; set; }
        public int MatchesCount { get; set; }
        public int InjuriesCount { get; set; }

        // Captured from COUNT(*) OVER()
        [JsonIgnore]
        public int TotalCount { get; set; }

    }
    */
}
