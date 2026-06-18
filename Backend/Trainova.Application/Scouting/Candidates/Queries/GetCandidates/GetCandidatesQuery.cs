using MediatR;
using Trainova.Application.Scouting.Candidates;
using Trainova.Domain.Common.Enums;
using System;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using System.Collections.Generic;

namespace Trainova.Application.Scouting.Candidates.Queries.GetCandidates
{
    [Authorize(Roles = "TeamStaff,HeadCoach,AssistantCoach,FitnessCoach")]
    public record GetCandidatesQuery(
        Guid? CandidateId = null,
        string? SearchTerm = null,
        int? Position = null,
        CandidateStatus? Status = null,
        string? CurrentTeamName = null,
        int? MinAge = null,
        int? MaxAge = null,
        DateTime? DateFrom = null,
        DateTime? DateTo = null,
        int PageNumber = 0,
        int PageSize = 12,
        string SortColumn = "CreatedAt",
        string SortDirection = "DESC") : IRequest<ResultOf<IEnumerable<CandidateListItemResponse>>>;
}
