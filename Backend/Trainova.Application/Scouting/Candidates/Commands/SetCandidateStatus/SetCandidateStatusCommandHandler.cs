using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Domain.Common.Enums;
using System.Threading;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;

namespace Trainova.Application.Scouting.Candidates.Commands.SetCandidateStatus
{
    public class SetCandidateStatusCommandHandler : IRequestHandler<SetCandidateStatusCommand, ResultOf<bool>>
    {
        private readonly ICandidateRepository _candidateRepository;

        public SetCandidateStatusCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<ResultOf<bool>> Handle(SetCandidateStatusCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
            if (candidate == null)
                return Error.NotFound("Candidate.NotFound", $"Candidate {request.CandidateId} not found").AsError<bool>();

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
