namespace Trainova.Api.Models
{
    public class Paginator
    {
        public Paginator(int page = 0, int pageSize = 12)
        {
            Page = page;
            PageSize = pageSize;
        }

        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 12;
    }
}
