using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Profiles.Candidates;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Profiles.Candidates.Commands.SetCandidateShortlist
{
    public class SetCandidateShortlistCommandHandler(
        ICandidateRepository _candidateRepository)
        : IRequestHandler<SetCandidateShortlistCommand, ResultOf<bool>>
    {
        public async Task<ResultOf<bool>> Handle(SetCandidateShortlistCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId);

                if (candidate == null)
                    return Error.NotFound(code: "SetCandidateShortlist_CandidateNotFound", description: "Candidate not found");

                await _candidateRepository.SetShortlistAsync(request.CandidateId, request.IsShortlisted);

                return true.AsDone();
            }
            catch (Exception ex)
            {
                return Error.Failure(code: "SetCandidateShortlist_Failed", description: ex.Message);
            }
        }
    }
}
