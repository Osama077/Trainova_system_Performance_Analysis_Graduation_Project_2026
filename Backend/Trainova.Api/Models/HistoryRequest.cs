namespace Trainova.Api.Models
{
    public class HistoryRequest : Paginator
    {
        public bool IncludeAdded {  get; set; }
        public bool IncludeDeleted { get; set; }
        public bool IncludeUpdated { get; set; }
    }
}
