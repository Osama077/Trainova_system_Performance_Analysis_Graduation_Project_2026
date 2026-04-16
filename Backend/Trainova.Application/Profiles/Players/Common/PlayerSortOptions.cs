namespace Trainova.Application.Profiles.Players.Common
{
    public static class PlayerCommonOptions
    {
        public const string CreatedAtSortOption = "CreatedAt";
        public const string FullNameSortOption = "FullName";
        public const string PerformanceSortOption = "PerformanceLevel";
        public const string MatchesSortOptions = "MatchesCount";
        public const string DateOfEnrolmentSortOption = "DateOfEnrolment";

        public static readonly IReadOnlyCollection<string> ValidSortColumns = new[]
        {
            CreatedAtSortOption,
            FullNameSortOption,
            "ShowName",
            PerformanceSortOption,
            "MedicalStatus",
            MatchesSortOptions,
            "InjuriesCount",
            DateOfEnrolmentSortOption
        };
    }
}
