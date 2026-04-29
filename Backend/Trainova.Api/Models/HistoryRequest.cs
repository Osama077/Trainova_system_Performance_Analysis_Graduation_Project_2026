using Trainova.Application.MedicalStatus.Injuries.Queries.GetInjuriesHistory;
using Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetPlayerInjuryHistory;

namespace Trainova.Api.Models
{
    public class HistoryRequest : Paginator
    {

        public IncludeHistoryType IncludeHistory { get; set; } = IncludeHistoryType.Deleted;

        public (bool includeAdded, bool includeDeleted, bool includeUpdated) ToHistoryFilter()
        {
            return (
                includeAdded: IncludeHistory.HasFlag(IncludeHistoryType.Added),
                includeDeleted: IncludeHistory.HasFlag(IncludeHistoryType.Deleted),
                includeUpdated: IncludeHistory.HasFlag(IncludeHistoryType.Updated)
            );
        }
        public GetInjuriesHistoryQuery ToInjuriesHistoryQuery(Guid? id = null)
        {
            var (includeAdded, includeDeleted, includeUpdated) = ToHistoryFilter();
            return new GetInjuriesHistoryQuery(
                Id: id,
                Page: Page,
                PageSize: PageSize,
                IncludeAdded: includeAdded,
                IncludeDeleted: includeDeleted,
                IncludeUpdated: includeUpdated
            );
        }
        public GetPlayerInjuryHistoryQuery ToPlayerInjuriesHistoryQuery(Guid? id = null)
        {
            var (includeAdded, includeDeleted, includeUpdated) = ToHistoryFilter();
            return new GetPlayerInjuryHistoryQuery(
                PlayerInjuryId: id,
                Page: Page,
                PageSize: PageSize,
                IncludeAdded: includeAdded,
                IncludeDeleted: includeDeleted,
                IncludeUpdated: includeUpdated
            );
        }
    }
}
