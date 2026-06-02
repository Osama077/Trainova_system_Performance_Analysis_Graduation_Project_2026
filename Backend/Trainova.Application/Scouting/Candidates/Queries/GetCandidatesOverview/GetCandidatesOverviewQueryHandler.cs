using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;

namespace Trainova.Application.Scouting.Candidates.Queries.GetCandidatesOverview
{
    public class GetCandidatesOverviewQueryHandler : IRequestHandler<GetCandidatesOverviewQuery, ResultOf<CandidatesOverviewResponse>>
    {
        private readonly ICandidateRepository _candidateRepository;

        public GetCandidatesOverviewQueryHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<ResultOf<CandidatesOverviewResponse>> Handle(GetCandidatesOverviewQuery request, CancellationToken cancellationToken)
        {
            // Validate paging
            if (request.PageNumber < 0 || request.PageSize <= 0 || request.PageSize > 500)
                return Error.Validation("GetCandidatesOverview.InvalidPaging", "PageNumber must be >= 0 and PageSize must be between 1 and 500").AsError<CandidatesOverviewResponse>();

            try
            {
                var response = await _candidateRepository.GetCandidatesOverviewAsync(
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
                    sortDirection: request.SortDirection,
                    cancellationToken: cancellationToken);

                return response.AsDone();
            }
            catch (InvalidOperationException invEx) // repository throws InvalidOperationException for DB mapping/sql issues
            {
                // Detect SQL-related inner exceptions without referencing Microsoft.Data.SqlClient here
                var inner = invEx.InnerException;
                // Build a detailed message including inner exception chain to help debugging
                var detail = invEx.Message;
                var current = inner;
                while (current != null)
                {
                    detail += " | " + current.Message;
                    current = current.InnerException;
                }

                // If any inner exception looks like a SQL error, use DbError code, otherwise general failed
                if (invEx.InnerException != null && invEx.InnerException.GetType().Name.IndexOf("SqlException", StringComparison.OrdinalIgnoreCase) >= 0)
                    return Error.Failure("GetCandidatesOverview.DbError", $"Database error while retrieving candidates overview: {detail}").AsError<CandidatesOverviewResponse>();

                return Error.Failure("GetCandidatesOverview.Failed", $"Failed to retrieve candidates overview: {detail}").AsError<CandidatesOverviewResponse>();
            }
            catch (Exception ex)
            {
                return Error.Failure("GetCandidatesOverview.Failed", $"Failed to retrieve candidates overview: {ex.Message}").AsError<CandidatesOverviewResponse>();
            }
        }
    }
}
