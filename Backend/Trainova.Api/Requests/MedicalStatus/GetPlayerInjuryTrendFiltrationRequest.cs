using Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetPlayerInjuriesTrend;

namespace Trainova.Api.Requests.MedicalStatus
{
    public class GetPlayerInjuryTrendFiltrationRequest
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

        public GetPlayerInjuriesTrendQuery ToTrendQuery()
        {
            return new GetPlayerInjuriesTrendQuery(
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
                ReturnedAfter);
        }
    }
}
