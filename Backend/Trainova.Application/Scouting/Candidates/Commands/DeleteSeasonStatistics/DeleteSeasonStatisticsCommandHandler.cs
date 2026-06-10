using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Trainova.Application.Scouting.Candidates.Commands.DeleteSeasonStatistics
{
    public class DeleteSeasonStatisticsCommandHandler : IRequestHandler<DeleteSeasonStatisticsCommand, ResultOf<bool>>
    {
        private readonly ICandidateRepository _candidateRepository;

        public DeleteSeasonStatisticsCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<ResultOf<bool>> Handle(DeleteSeasonStatisticsCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
            if (candidate == null)
                return Error.NotFound("Candidate.NotFound", $"Candidate {request.CandidateId} not found").AsError<bool>();

            var removed = candidate.RemoveSeason(request.SeasonId);
            if (!removed)
                return Error.NotFound("Season.NotFound", $"Season statistics {request.SeasonId} not found").AsError<bool>();

            try
            {
                await _candidateRepository.UpdateAsync(candidate, cancellationToken);
                await _candidateRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return Error.Failure("DeleteSeasonStatistics.Failed", $"Failed to delete season statistics {request.SeasonId} for candidate {request.CandidateId}: {ex.Message}").AsError<bool>();
            }

            return true.AsDone();
        }
    }
}
