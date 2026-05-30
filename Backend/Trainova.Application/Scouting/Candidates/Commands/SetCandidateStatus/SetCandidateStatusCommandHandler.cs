using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Domain.Common.Enums;
using System.Threading;

namespace Trainova.Application.Scouting.Candidates.Commands.SetCandidateStatus
{
    public class SetCandidateStatusCommandHandler : IRequestHandler<SetCandidateStatusCommand, bool>
    {
        private readonly ICandidateRepository _candidateRepository;

        public SetCandidateStatusCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<bool> Handle(SetCandidateStatusCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
            if (candidate == null) return false;

            if (request.Add)
                candidate.AddStatus(request.Flags);
            else
                candidate.RemoveStatus(request.Flags);

            // Business rule example: if Rejected was added, clear Shortlisted and OnTrial
            if (request.Add && (request.Flags & CandidateStatus.Rejected) != 0)
            {
                candidate.RemoveStatus(CandidateStatus.Shortlisted | CandidateStatus.OnTrial);
            }

            try
            {
                await _candidateRepository.UpdateAsync(candidate);
                await _candidateRepository.SaveChangesAsync(cancellationToken);
            }
            catch (System.Exception ex)
            {
                // Wrap and surface a clear message for callers (middleware will translate to a 400/500 as appropriate)
                throw new System.InvalidOperationException($"Failed to update candidate status for candidate {request.CandidateId}: {ex.Message}", ex);
            }

            return true;
        }
    }
}
