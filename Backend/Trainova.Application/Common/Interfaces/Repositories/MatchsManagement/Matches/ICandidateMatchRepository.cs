using Trainova.Application.MatchsManagement.Matches;

namespace Trainova.Application.Common.Interfaces.Repositories.MatchsManagement.Matches
{
    public interface ICandidateMatchRepository
    {
        Task AddAsync(Trainova.Domain.MatchsManagement.Matches.CandidateMatch match);
        Task UpdateAsync(Trainova.Domain.MatchsManagement.Matches.CandidateMatch match);
        Task<IEnumerable<CandidateMatchResponse>> GetMatchesAsync(Guid? candidateId = null, DateTime? dateFrom = null, DateTime? dateTo = null, int pageNumber = 0, int pageSize = 12);
        Task<Trainova.Domain.MatchsManagement.Matches.CandidateMatch?> GetByIdAsync(Guid id);
    }
}
