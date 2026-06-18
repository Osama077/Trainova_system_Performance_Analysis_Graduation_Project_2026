using MediatR;
using System;
using System.Collections.Generic;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Queries.GetCandidateNotes
{
    [Authorize(Roles = "TeamStaff,HeadCoach,AssistantCoach,FitnessCoach")]
    public record GetCandidateNotesQuery(Guid CandidateId, int PageNumber = 0, int PageSize = 50) : IRequest<ResultOf<IEnumerable<CandidateNoteResponse>>>;
}
