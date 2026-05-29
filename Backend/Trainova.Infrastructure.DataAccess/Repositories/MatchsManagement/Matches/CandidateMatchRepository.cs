using Microsoft.EntityFrameworkCore;
using Trainova.Application.Common.Interfaces.Repositories.MatchsManagement.Matches;
using Trainova.Application.MatchsManagement.Matches;
using Trainova.Domain.MatchsManagement.Matches;

namespace Trainova.Infrastructure.DataAccess.Repositories.MatchsManagement.Matches
{
    public class CandidateMatchRepository : ICandidateMatchRepository
    {
        private readonly TrainovaWriteDbContext _dbContext;

        public CandidateMatchRepository(TrainovaWriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(CandidateMatch match)
        {
            await _dbContext.AddAsync(match);
        }

        public async Task<CandidateMatch?> GetByIdAsync(Guid id)
        {
            return await _dbContext.CandidateMatches.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<CandidateMatchResponse>> GetMatchesAsync(Guid? candidateId = null, DateTime? dateFrom = null, DateTime? dateTo = null, int pageNumber = 0, int pageSize = 12)
        {
            var q = _dbContext.CandidateMatches.AsQueryable();
            if (candidateId.HasValue) q = q.Where(m => m.CandidateId == candidateId.Value);
            if (dateFrom.HasValue) q = q.Where(m => m.MatchDate >= dateFrom.Value);
            if (dateTo.HasValue) q = q.Where(m => m.MatchDate <= dateTo.Value);

            var items = await q
                .OrderByDescending(m => m.MatchDate)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .Select(m => new CandidateMatchResponse
                {
                    Id = m.Id,
                    CandidateId = m.CandidateId,
                    MatchDate = m.MatchDate,
                    OpponentName = m.OpponentName,
                    HomeScore = m.HomeScore,
                    AwayScore = m.AwayScore,
                    Notes = m.Notes
                })
                .ToListAsync();

            return items;
        }

        public Task UpdateAsync(CandidateMatch match)
        {
            _dbContext.Update(match);
            return Task.CompletedTask;
        }
    }
}
