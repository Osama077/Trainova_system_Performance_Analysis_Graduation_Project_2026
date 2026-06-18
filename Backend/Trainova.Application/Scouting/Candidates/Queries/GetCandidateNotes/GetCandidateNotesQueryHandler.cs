using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using System.Linq;

namespace Trainova.Application.Scouting.Candidates.Queries.GetCandidateNotes
{
    public class GetCandidateNotesQueryHandler : IRequestHandler<GetCandidateNotesQuery, ResultOf<IEnumerable<CandidateNoteResponse>>>
    {
        private readonly ICandidateRepository _candidateRepository;

        public GetCandidateNotesQueryHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<ResultOf<IEnumerable<CandidateNoteResponse>>> Handle(GetCandidateNotesQuery request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
            if (candidate == null)
                return Error.NotFound("Candidate.NotFound", $"Candidate {request.CandidateId} not found").AsError<IEnumerable<CandidateNoteResponse>>();

            var totalCount = candidate.NotesList.Count;

            var notes = candidate.NotesList
                .OrderByDescending(n => n.CreatedAt)
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .Select(n => new CandidateNoteResponse
                {
                    Id = n.Id,
                    Text = n.Text,
                    CreatedBy = n.CreatedBy,
                    CreatedByName = n.CreatedByName,
                    CreatedAt = n.CreatedAt,
                    TotalCount = totalCount
                });

            return notes.AsPartial();
        }
    }
}
