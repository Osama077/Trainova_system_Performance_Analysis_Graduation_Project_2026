namespace Trainova.Application.Common.Helpers
{
    public static class GeneralSortHelper
    {
        public const string ASCSortOption = "ASC";
        public const string DESCSortOption = "DESC";
        public static IReadOnlyCollection<string> SortDirectionOptions { get; } = new[]
        {
            ASCSortOption,
            DESCSortOption
        };
    }
}
