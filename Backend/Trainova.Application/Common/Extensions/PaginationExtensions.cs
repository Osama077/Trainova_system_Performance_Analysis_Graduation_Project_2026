using Trainova.Application.Common.Interfaces.MarkUps;

namespace Trainova.Application.Common.Extensions
{
    public static class PaginationExtensions
    {
        public static int ExtractTotalCount<T>(IEnumerable<T> items)
            where T : ITotalCountIncluded
        {
            return items.FirstOrDefault()?.TotalCount ?? 0;
        }


    }
}
