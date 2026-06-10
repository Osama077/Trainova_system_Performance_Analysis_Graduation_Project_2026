using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Trainova.Application.Scouting.Candidates.Commands.AddCandidateMatch
{
    public class AddCandidateMatchCommandHandler : IRequestHandler<AddCandidateMatchCommand, ResultOf<Guid>>
    {
        private readonly ICandidateRepository _candidateRepository;

        public AddCandidateMatchCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<ResultOf<Guid>> Handle(AddCandidateMatchCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
            if (candidate == null)
                return Error.NotFound("Candidate.NotFound", $"Candidate {request.CandidateId} not found").AsError<Guid>();

            var matchId = candidate.AddMatch(
                request.MatchDate,
                request.MatchName,
                request.Goals,
                request.Assists,
                request.Rating,
                request.ScoutNotes);

            try
            {
                await _candidateRepository.UpdateAsync(candidate, cancellationToken);
                await _candidateRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return Error.Failure("AddCandidateMatch.Failed", $"Failed to add match for candidate {request.CandidateId}: {ex.Message}").AsError<Guid>();
            }

            return matchId.AsDone();
        }
    }
}
