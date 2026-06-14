using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;

namespace Trainova.Application.Scouting.Candidates.Commands.DeleteCandidate
{
    public class DeleteCandidateCommandHandler : IRequestHandler<DeleteCandidateCommand, ResultOf<bool>>
    {
        private readonly ICandidateRepository _candidateRepository;

        public DeleteCandidateCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<ResultOf<bool>> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
            if (candidate == null)
                return Error.NotFound("Candidate.NotFound", $"Candidate {request.CandidateId} not found").AsError<bool>();

            try
            {
                await _candidateRepository.DeleteAsync(candidate, cancellationToken);
                await _candidateRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return Error.Failure("DeleteCandidate.Failed", $"Failed to delete candidate {request.CandidateId}: {ex.Message}").AsError<bool>();
            }

            return true.AsDone();
        }
    }
}
