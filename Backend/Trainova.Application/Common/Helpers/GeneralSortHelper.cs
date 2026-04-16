namespace Trainova.Application.Common.Helpers
{
    public static class GeneralSortHelper
    {
        public static string ASCSortOption { get; } = "ASC";
        public static string DESCSortOption { get; } = "DESC";
        public static IReadOnlyCollection<string> SortDirectionOptions { get; } = new[]
        {
            "ASC",
            "DESC"
        };
    }
}
