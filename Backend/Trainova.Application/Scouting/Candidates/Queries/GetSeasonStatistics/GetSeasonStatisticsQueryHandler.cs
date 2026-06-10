using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Trainova.Application.Scouting.Candidates.Queries.GetSeasonStatistics
{
    public class GetSeasonStatisticsQueryHandler : IRequestHandler<GetSeasonStatisticsQuery, ResultOf<IEnumerable<SeasonStatisticsResponse>>>
    {
        private readonly ICandidateRepository _candidateRepository;

        public GetSeasonStatisticsQueryHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<ResultOf<IEnumerable<SeasonStatisticsResponse>>> Handle(GetSeasonStatisticsQuery request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
            if (candidate == null)
                return Error.NotFound("Candidate.NotFound", $"Candidate {request.CandidateId} not found").AsError<IEnumerable<SeasonStatisticsResponse>>();

            var seasons = candidate.SeasonsList
                .Select(s => new SeasonStatisticsResponse
                {
                    Id = s.Id,
                    CandidateId = s.CandidateId,
                    Season = s.Season,
                    League = s.League,
                    Goals = s.Goals,
                    Assists = s.Assists,
                    Matches = s.Matches,
                    PassAccuracy = s.PassAccuracy,
                    ShotsPer90 = s.ShotsPer90,
                    XgPer90 = s.XgPer90
                });

            return seasons.AsDone();
        }
    }
}
