using Trainova.Application.Profiles.Candidates;

namespace Trainova.Application.Common.Interfaces.Repositories.Profiles.Candidates
{
    public interface ICandidateRepository
    {
        Task AddAsync(Trainova.Domain.Profiles.Candidate candidate);
        Task UpdateAsync(Trainova.Domain.Profiles.Candidate candidate);
        Task<IEnumerable<CandidateDetailResponse>> GetCandidatesAsync(
            Guid? candidateId = null,
            string? searchTerm = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            int pageNumber = 0,
            int pageSize = 12,
            string sortColumn = "ScoutedAt",
            string sortDirection = "DESC");
        Task<Trainova.Domain.Profiles.Candidate?> GetByIdAsync(Guid candidateId);
        Task SetShortlistAsync(Guid candidateId, bool isShortlisted);
    }
}
