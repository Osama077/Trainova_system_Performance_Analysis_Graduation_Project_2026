using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.MatchsManagement.Matches;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Application.MatchsManagement.Matches.Queries.GetCandidateMatches
{
    public class GetCandidateMatchesQueryHandler(
        ICandidateMatchRepository _candidateMatchRepository)
        : IRequestHandler<GetCandidateMatchesQuery, ResultOf<IEnumerable<Trainova.Application.MatchsManagement.Matches.CandidateMatchResponse>>>
    {
        public async Task<ResultOf<IEnumerable<Trainova.Application.MatchsManagement.Matches.CandidateMatchResponse>>> Handle(GetCandidateMatchesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var items = await _candidateMatchRepository.GetMatchesAsync(
                    candidateId: request.CandidateId,
                    dateFrom: request.DateFrom,
                    dateTo: request.DateTo,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize);

                if (!items.Any())
                    return items.AsZeroCount();

                return items.AsPartial();
            }
            catch(Exception ex)
            {
                return Error.Failure(code: "GetCandidateMatches_Failed", description: ex.Message);
            }
        }
    }
}
