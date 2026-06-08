using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Domain.Common.Enums;
using System.Threading;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using Trainova.Application.Common.Models;

namespace Trainova.Application.Scouting.Candidates.Commands.SetCandidateStatus
{
    public class SetCandidateStatusCommandHandler : IRequestHandler<SetCandidateStatusCommand, ResultOf<bool>>
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly CurrentUser? _currentUser;

        public SetCandidateStatusCommandHandler(ICandidateRepository candidateRepository, CurrentUser? currentUser = null)
        {
            _candidateRepository = candidateRepository;
            _currentUser = currentUser;
        }

        public async Task<ResultOf<bool>> Handle(SetCandidateStatusCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
            if (candidate == null)
                return Error.NotFound("Candidate.NotFound", $"Candidate {request.CandidateId} not found").AsError<bool>();

            // Apply status change: add or remove selected flags
            if (request.Add)
                candidate.AddStatus(request.Status);
            else
                candidate.RemoveStatus(request.Status);

            try
            {
                await _candidateRepository.UpdateAsync(candidate, cancellationToken);
                await _candidateRepository.SaveChangesAsync(cancellationToken);
            }
            catch (System.Exception ex)
            {
                return Error.Failure("SetCandidateStatus.Failed", $"Failed to update candidate status for candidate {request.CandidateId}: {ex.Message}").AsError<bool>();
            }

            return true.AsDone();
        }
    }
}
