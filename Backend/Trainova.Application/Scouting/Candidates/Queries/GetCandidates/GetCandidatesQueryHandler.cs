using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Domain.Common.Enums;
using Trainova.Common.ResultOf;
using System.Collections.Generic;
using Trainova.Common.Errors;

namespace Trainova.Application.Scouting.Candidates.Queries.GetCandidates
{
    public class GetCandidatesQueryHandler : IRequestHandler<GetCandidatesQuery, ResultOf<IEnumerable<CandidateListItemResponse>>>
    {
        private readonly ICandidateRepository _candidateRepository;

        public GetCandidatesQueryHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<ResultOf<IEnumerable<CandidateListItemResponse>>> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var list = await _candidateRepository.GetCandidatesAsync(
                    candidateId: request.CandidateId,
                    searchTerm: request.SearchTerm,
                    mainPositionFilter: request.Position,
                    statusFilter: request.Status,
                    currentTeamName: request.CurrentTeamName,
                    minAge: request.MinAge,
                    maxAge: request.MaxAge,
                    dateFrom: request.DateFrom,
                    dateTo: request.DateTo,
                    pageNumber: request.PageNumber,
                    pageSize: request.PageSize,
                    sortColumn: request.SortColumn,
                    sortDirection: request.SortDirection
                    );

                return list.AsDone();
            }
            catch (System.Exception ex)
            {
                return Error.Failure("GetCandidates.Failed", $"Failed to retrieve candidates: {ex.Message}").AsError<IEnumerable<CandidateListItemResponse>>();
            }
        }
    }
}
