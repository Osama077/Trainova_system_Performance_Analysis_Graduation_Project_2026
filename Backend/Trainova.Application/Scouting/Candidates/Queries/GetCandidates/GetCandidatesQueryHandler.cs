using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Domain.Common.Enums;

namespace Trainova.Application.Scouting.Candidates.Queries.GetCandidates
{
    public class GetCandidatesQueryHandler : IRequestHandler<GetCandidatesQuery, IEnumerable<CandidateListItemResponse>>
    {
        private readonly ICandidateRepository _candidateRepository;

        public GetCandidatesQueryHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<IEnumerable<CandidateListItemResponse>> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
        {
            return await _candidateRepository.GetCandidatesAsync(
                candidateId: request.CandidateId,
                searchTerm: request.SearchTerm,
                mainPositionFilter: request.Position,
                statusFilter: request.Status,
                currentTeamId: request.CurrentTeamId,
                minAge: request.MinAge,
                maxAge: request.MaxAge,
                dateFrom: request.DateFrom,
                dateTo: request.DateTo,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                sortColumn: request.SortColumn,
                sortDirection: request.SortDirection
                );
        }
    }
}
