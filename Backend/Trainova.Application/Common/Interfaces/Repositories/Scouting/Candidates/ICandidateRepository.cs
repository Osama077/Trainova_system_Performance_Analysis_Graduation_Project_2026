using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Trainova.Application.Scouting.Candidates;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.Scouting;

namespace Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates
{
    public interface ICandidateRepository
    {
        Task AddAsync(ScoutingCandidate candidate, CancellationToken cancellationToken = default);
        Task UpdateAsync(ScoutingCandidate candidate, CancellationToken cancellationToken = default);
        Task<ScoutingCandidate?> GetByIdAsync(Guid candidateId, CancellationToken cancellationToken = default);
        Task<IEnumerable<CandidateListItemResponse>> GetCandidatesAsync(
            Guid? candidateId = null,
            string? searchTerm = null,
            int? mainPositionFilter = null,
            CandidateStatus? statusFilter = null,
            string? currentTeamName = null,
            int? minAge = null,
            int? maxAge = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            int pageNumber = 0,
            int pageSize = 12,
            string sortColumn = "CreatedAt",
            string sortDirection = "DESC",
            CancellationToken cancellationToken = default);

        // New: combined overview + filtered/paged list response
        Task<CandidatesOverviewResponse> GetCandidatesOverviewAsync(
            Guid? candidateId = null,
            string? searchTerm = null,
            int? mainPositionFilter = null,
            CandidateStatus? statusFilter = null,
            string? currentTeamName = null,
            int? minAge = null,
            int? maxAge = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            int pageNumber = 0,
            int pageSize = 12,
            string sortColumn = "CreatedAt",
            string sortDirection = "DESC",
            CancellationToken cancellationToken = default);

        // Persist any pending changes to the database asynchronously
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
