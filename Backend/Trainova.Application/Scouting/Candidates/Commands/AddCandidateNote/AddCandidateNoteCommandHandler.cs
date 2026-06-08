using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using Trainova.Application.Common.Models;

namespace Trainova.Application.Scouting.Candidates.Commands.AddCandidateNote
{
    public class AddCandidateNoteCommandHandler : IRequestHandler<AddCandidateNoteCommand, ResultOf<Guid>>
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly CurrentUser? _currentUser;

        public AddCandidateNoteCommandHandler(ICandidateRepository candidateRepository, CurrentUser? currentUser = null)
        {
            _candidateRepository = candidateRepository;
            _currentUser = currentUser;
        }

        public async Task<ResultOf<Guid>> Handle(AddCandidateNoteCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
            if (candidate == null)
                return Error.NotFound("Candidate.NotFound", $"Candidate {request.CandidateId} not found").AsError<Guid>();

            var noteId = candidate.AddNote(request.Text, _currentUser?.Id, _currentUser?.Name);

            try
            {
                await _candidateRepository.UpdateAsync(candidate, cancellationToken);
                await _candidateRepository.SaveChangesAsync(cancellationToken);
            }
            catch (System.Exception ex)
            {
                return Error.Failure("AddCandidateNote.Failed", $"Failed to add note for candidate {request.CandidateId}: {ex.Message}").AsError<Guid>();
            }

            return noteId.AsDone();
        }
    }
}
