using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Profiles.Candidates;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Profiles.Candidates.Queries.GetCandidates
{
    public class GetCandidatesQueryHandler(
        ICandidateRepository _candidateRepository)
        : IRequestHandler<GetCandidatesQuery, ResultOf<IEnumerable<Trainova.Application.Profiles.Candidates.CandidateDetailResponse>>>
    {
        public async Task<ResultOf<IEnumerable<Trainova.Application.Profiles.Candidates.CandidateDetailResponse>>> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var items = await _candidateRepository.GetCandidatesAsync(
                    candidateId: request.CandidateId,
                    searchTerm: request.SearchTerm,
                    dateFrom: request.DateFrom,
                    dateTo: request.DateTo,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize,
                    sortColumn: request.SortColumn,
                    sortDirection: request.SortDirection);

                if (!items.Any())
                {
                    return items.AsZeroCount();
                }

                return items.AsPartial();
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "GetCandidatesQueryHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }
}
