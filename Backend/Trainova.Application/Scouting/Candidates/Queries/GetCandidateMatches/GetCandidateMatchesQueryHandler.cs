using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Trainova.Application.Scouting.Candidates.Queries.GetCandidateMatches
{
    public class GetCandidateMatchesQueryHandler : IRequestHandler<GetCandidateMatchesQuery, ResultOf<IEnumerable<CandidateMatchResponse>>>
    {
        private readonly ICandidateRepository _candidateRepository;

        public GetCandidateMatchesQueryHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<ResultOf<IEnumerable<CandidateMatchResponse>>> Handle(GetCandidateMatchesQuery request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
            if (candidate == null)
                return Error.NotFound("Candidate.NotFound", $"Candidate {request.CandidateId} not found").AsError<IEnumerable<CandidateMatchResponse>>();

            var matches = candidate.MatchesList
                .OrderByDescending(m => m.MatchDate)
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .Select(m => new CandidateMatchResponse
                {
                    Id = m.Id,
                    CandidateId = m.CandidateId,
                    MatchDate = m.MatchDate,
                    MatchName = m.MatchName,
                    Goals = m.Goals,
                    Assists = m.Assists,
                    Rating = m.Rating,
                    ScoutNotes = m.ScoutNotes
                });

            return matches.AsDone();
        }
    }
}
