namespace Trainova.Api.Models
{
    [Flags]
    public enum IncludeHistoryType
    {
        None = 0,
        Added = 1,
        Deleted = 2,
        Updated = 4
    }
}
