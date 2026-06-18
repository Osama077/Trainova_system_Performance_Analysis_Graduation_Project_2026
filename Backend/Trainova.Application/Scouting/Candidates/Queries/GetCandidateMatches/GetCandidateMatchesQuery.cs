using MediatR;
using System;
using System.Collections.Generic;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Queries.GetCandidateMatches
{
    [Authorize(Roles = "TeamStaff,HeadCoach,AssistantCoach,FitnessCoach")]
    public record GetCandidateMatchesQuery(Guid CandidateId, int PageNumber = 0, int PageSize = 50) : IRequest<ResultOf<IEnumerable<CandidateMatchResponse>>>;
}
