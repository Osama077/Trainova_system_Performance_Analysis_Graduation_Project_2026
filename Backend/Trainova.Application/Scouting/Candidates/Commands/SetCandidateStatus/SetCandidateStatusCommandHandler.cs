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

            // Apply status change: add or remove selected flags
            // Apply status change: when Add=true we now REPLACE the existing flags with the provided status
            // (i.e. the new flag erases the old). When Add=false we remove the provided flags as before.
            if (request.Add)
                candidate.SetStatus(request.Status);
            else
                candidate.RemoveStatus(request.Status);

            // Persist note changes if provided (replace existing notes)
            if (!string.IsNullOrWhiteSpace(request.Note))
            {
                candidate.Update(notes: request.Note);
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
