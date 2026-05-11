using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Queries.GetPlayerInjuries
{
    [Authorize(Role = "Doctor,Player,SystemAdmin,HeadCoach,AssistantCoach")]
    public record GetPlayerInjuriesQuery : IRequest<ResultOf<IEnumerable<PlayerInjuryReadModel>>>, IPlayerAuthraizedRequest
    {
        public Guid? PlayerId { get; private set; }

        Guid? IPlayerAuthraizedRequest.PlayerId
        {
            get => PlayerId;
            set => PlayerId = value;
        }

        public Guid? InjuryId { get; init; }
        public string? Status { get; init; }
        public string? Cause { get; init; }
        public bool? IsNew { get; init; }

        public DateTime? HappendBefore { get; init; }
        public DateTime? HappendAfter { get; init; }

        public DateTime? ExpectedReturnBefore { get; init; }
        public DateTime? ExpectedReturnAfter { get; init; }

        public DateTime? ReturnedBefore { get; init; }
        public DateTime? ReturnedAfter { get; init; }

        public int Page { get; init; } = 0;
        public int PageSize { get; init; } = 12;

        public string? SortColumn { get; init; }
        public string? SortDirection { get; init; }

        public GetPlayerInjuriesQuery(
            Guid? playerId = null,
            Guid? injuryId = null,
            string? status = null,
            string? cause = null,
            bool? isNew = null,
            DateTime? happendBefore = null,
            DateTime? happendAfter = null,
            DateTime? expectedReturnBefore = null,
            DateTime? expectedReturnAfter = null,
            DateTime? returnedBefore = null,
            DateTime? returnedAfter = null,
            int page = 0,
            int pageSize = 12,
            string? sortColumn = null,
            string? sortDirection = null)
        {
            PlayerId = playerId;
            InjuryId = injuryId;
            Status = status;
            Cause = cause;
            IsNew = isNew;
            HappendBefore = happendBefore;
            HappendAfter = happendAfter;
            ExpectedReturnBefore = expectedReturnBefore;
            ExpectedReturnAfter = expectedReturnAfter;
            ReturnedBefore = returnedBefore;
            ReturnedAfter = returnedAfter;
            Page = page;
            PageSize = pageSize;
            SortColumn = sortColumn;
            SortDirection = sortDirection;
        }

    }

}
