using Trainova.Api.Models;
using Trainova.Application.MedicalStatus.Injuries.Queries.GetInjuries;
using Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetPlayerInjuries;
using Trainova.Domain.MedicalStatus.PlayerInjuries;

namespace Trainova.Api.Requsts.MedicalStatus.PlayerInjuries
{
    public class GetPlayerInjuryFiltrationRequest : Paginator
    {
        public Guid? PlayerId { get; set; } = null;
        public Guid? InjuryId { get; set; } = null;
        public string? Status { get; set; } = null;
        public string? Cause { get; set; } = null;
        public bool? IsNew { get; set; } = null;
        public DateTime? HappendBefore { get; set; } = null;
        public DateTime? HappendAfter { get; set; } = null;
        public DateTime? ExpectedReturnBefore { get; set; } = null;
        public DateTime? ExpectedReturnAfter { get; set; } = null;
        public DateTime? ReturnedBefore { get; set; } = null;
        public DateTime? ReturnedAfter { get; set; } = null;
        public GetPlayerInjuriesQuery ToQuery(Guid? id)
        {
            return new GetPlayerInjuriesQuery(
                id,
                PlayerId,
                InjuryId,
                Status,
                Cause,
                IsNew,
                HappendBefore,
                HappendAfter,
                ExpectedReturnBefore,
                ExpectedReturnAfter,
                ReturnedBefore,
                ReturnedAfter,
                Page, PageSize);
        }
    }
}
