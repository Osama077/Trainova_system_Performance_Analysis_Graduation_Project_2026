using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Trainova.Application.Scouting.Candidates.Commands.DeleteCandidateMatch
{
    public class DeleteCandidateMatchCommandHandler : IRequestHandler<DeleteCandidateMatchCommand, ResultOf<bool>>
    {
        private readonly ICandidateRepository _candidateRepository;

        public DeleteCandidateMatchCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<ResultOf<bool>> Handle(DeleteCandidateMatchCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
            if (candidate == null)
                return Error.NotFound("Candidate.NotFound", $"Candidate {request.CandidateId} not found").AsError<bool>();

            var removed = candidate.RemoveMatch(request.MatchId);
            if (!removed)
                return Error.NotFound("Match.NotFound", $"Match {request.MatchId} not found").AsError<bool>();

            try
            {
                await _candidateRepository.UpdateAsync(candidate, cancellationToken);
                await _candidateRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return Error.Failure("DeleteCandidateMatch.Failed", $"Failed to delete match {request.MatchId} for candidate {request.CandidateId}: {ex.Message}").AsError<bool>();
            }

            return true.AsDone();
        }
    }
}
