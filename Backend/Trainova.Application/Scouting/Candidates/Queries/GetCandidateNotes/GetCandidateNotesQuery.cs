using MediatR;
using System;
using System.Collections.Generic;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Scouting.Candidates.Queries.GetCandidateNotes
{
    public record GetCandidateNotesQuery(Guid CandidateId, int PageNumber = 0, int PageSize = 50) : IRequest<ResultOf<IEnumerable<CandidateNoteResponse>>>;
}
