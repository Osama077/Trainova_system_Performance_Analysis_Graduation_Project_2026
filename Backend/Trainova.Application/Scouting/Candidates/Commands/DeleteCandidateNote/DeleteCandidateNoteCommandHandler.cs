using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;

namespace Trainova.Application.Scouting.Candidates.Commands.DeleteCandidateNote
{
    public class DeleteCandidateNoteCommandHandler : IRequestHandler<DeleteCandidateNoteCommand, ResultOf<bool>>
    {
        private readonly ICandidateRepository _candidateRepository;

        public DeleteCandidateNoteCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<ResultOf<bool>> Handle(DeleteCandidateNoteCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
            if (candidate == null)
                return Error.NotFound("Candidate.NotFound", $"Candidate {request.CandidateId} not found").AsError<bool>();

            var removed = candidate.RemoveNote(request.NoteId);
            if (!removed) return Error.NotFound("Note.NotFound", $"Note {request.NoteId} not found").AsError<bool>();

            try
            {
                await _candidateRepository.UpdateAsync(candidate, cancellationToken);
                await _candidateRepository.SaveChangesAsync(cancellationToken);
            }
            catch (System.Exception ex)
            {
                return Error.Failure("DeleteCandidateNote.Failed", $"Failed to delete note {request.NoteId} for candidate {request.CandidateId}: {ex.Message}").AsError<bool>();
            }

            return true.AsDone();
        }
    }
}
