using Trainova.Application.Scouting.Candidates.Queries.GetCandidates;
using Trainova.Domain.Common.Enums;
using System;

namespace Trainova.Api.Requests.Scouting
{
    public class GetCandidatesFiltrationRequest
    {
        public Guid? CandidateId { get; init; }
        public string? CurrentTeamName { get; init; }
        public int? MinAge { get; init; }
        public int? MaxAge { get; init; }
        public string? SearchTerm { get; init; }
        public int? Position { get; init; }
        public CandidateStatus? Status { get; init; }
        public DateTime? DateFrom { get; init; }
        public DateTime? DateTo { get; init; }
        public int PageNumber { get; init; } = 0;
        public int PageSize { get; init; } = 12;
        public string SortColumn { get; init; } = "CreatedAt";
        public string SortDirection { get; init; } = "DESC";

        public GetCandidatesQuery ToQuery(Guid? candidateId) => new GetCandidatesQuery(
            CandidateId: candidateId ?? CandidateId,
            CurrentTeamName: CurrentTeamName,
            MinAge: MinAge,
            MaxAge: MaxAge,
            SearchTerm: SearchTerm,
            Position: Position,
            Status: Status,
            DateFrom: DateFrom,
            DateTo: DateTo,
            PageNumber: PageNumber,
            PageSize: PageSize,
            SortColumn: SortColumn,
            SortDirection: SortDirection
            );

        public Trainova.Application.Scouting.Candidates.Queries.GetCandidatesOverview.GetCandidatesOverviewQuery ToOverviewQuery()
            => new Trainova.Application.Scouting.Candidates.Queries.GetCandidatesOverview.GetCandidatesOverviewQuery(
                CandidateId: CandidateId,
                CurrentTeamName: CurrentTeamName,
                MinAge: MinAge,
                MaxAge: MaxAge,
                SearchTerm: SearchTerm,
                Position: Position,
                Status: Status,
                DateFrom: DateFrom,
                DateTo: DateTo,
                PageNumber: PageNumber,
                PageSize: PageSize,
                SortColumn: SortColumn,
                SortDirection: SortDirection
            );
    }
}
