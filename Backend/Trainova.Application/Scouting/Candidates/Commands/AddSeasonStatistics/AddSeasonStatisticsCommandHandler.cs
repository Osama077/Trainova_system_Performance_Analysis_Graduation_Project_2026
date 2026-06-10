using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Trainova.Application.Scouting.Candidates.Commands.AddSeasonStatistics
{
    public class AddSeasonStatisticsCommandHandler : IRequestHandler<AddSeasonStatisticsCommand, ResultOf<Guid>>
    {
        private readonly ICandidateRepository _candidateRepository;

        public AddSeasonStatisticsCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<ResultOf<Guid>> Handle(AddSeasonStatisticsCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
            if (candidate == null)
                return Error.NotFound("Candidate.NotFound", $"Candidate {request.CandidateId} not found").AsError<Guid>();

            var seasonId = candidate.AddSeason(
                request.Season,
                request.League,
                request.Goals,
                request.Assists,
                request.Matches,
                request.PassAccuracy,
                request.ShotsPer90,
                request.XgPer90);

            try
            {
                await _candidateRepository.UpdateAsync(candidate, cancellationToken);
                await _candidateRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return Error.Failure("AddSeasonStatistics.Failed", $"Failed to add season statistics for candidate {request.CandidateId}: {ex.Message}").AsError<Guid>();
            }

            return seasonId.AsDone();
        }
    }
}
