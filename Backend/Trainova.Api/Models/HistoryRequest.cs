using Trainova.Application.MedicalStatus.Injuries.Queries.GetInjuriesHistory;
using Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetPlayerInjuryHistory;

namespace Trainova.Api.Models
{
    public class HistoryRequest : Paginator
    {
        public bool IncludeAdded { get; set; }
        public bool IncludeDeleted { get; set; }
        public bool IncludeUpdated { get; set; }
        public GetInjuriesHistoryQuery ToInjuriesHistoryQuery(Guid? id = null)
        {
            return new GetInjuriesHistoryQuery(
                Id: id,
                Page: Page,
                PageSize: PageSize,
                IncludeAdded: IncludeAdded,
                IncludeDeleted: IncludeDeleted,
                IncludeUpdated: IncludeUpdated
            );
        }
        public GetPlayerInjuryHistoryQuery ToPlayerInjuriesHistoryQuery(Guid? id = null)
        {
            return new GetPlayerInjuryHistoryQuery(
                PlayerInjuryId: id,
                Page: Page,
                PageSize: PageSize,
                IncludeAdded: IncludeAdded,
                IncludeDeleted: IncludeDeleted,
                IncludeUpdated: IncludeUpdated
            );
        }
    }
}
