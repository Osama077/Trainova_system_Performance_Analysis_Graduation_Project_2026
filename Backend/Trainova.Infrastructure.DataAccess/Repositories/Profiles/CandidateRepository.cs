using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Trainova.Application.Common.Interfaces.Repositories.Profiles.Candidates;
using Trainova.Application.Profiles.Candidates;
using Trainova.Domain.Profiles;
using Trainova.Infrastructure.DataAccess.DbSettingsObjects;

namespace Trainova.Infrastructure.DataAccess.Repositories.Profiles
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly IDbSettings _dbSettings;
        private readonly TrainovaWriteDbContext _dbContext;

        public CandidateRepository(TrainovaWriteDbContext dbContext, IDbSettings dbSettings)
        {
            _dbContext = dbContext;
            _dbSettings = dbSettings;
        }

        public async Task AddAsync(Candidate candidate)
        {
            await _dbContext.AddAsync(candidate);
        }

        public async Task<Candidate?> GetByIdAsync(Guid candidateId)
        {
            return await _dbContext.Candidates.FirstOrDefaultAsync(c => c.Id == candidateId);
        }

        public async Task<IEnumerable<CandidateDetailResponse>> GetCandidatesAsync(
            Guid? candidateId = null,
            string? searchTerm = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            int pageNumber = 0,
            int pageSize = 12,
            string sortColumn = "ScoutedAt",
            string sortDirection = "DESC")
        {
            var parameters = new
            {
                CandidateId = candidateId,
                SearchTerm = searchTerm,
                DateFrom = dateFrom,
                DateTo = dateTo,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortColumn = sortColumn,
                SortDirection = sortDirection
            };

            var sql = "candidatesData.sp_GetCandidatesFiltered";

            using var conn = _dbSettings.CreateReadingConnection();

            return await conn.QueryAsync<CandidateDetailResponse>(
                sql: sql,
                param: parameters,
                commandType: CommandType.StoredProcedure,
                commandTimeout: 5
                );
        }

        public Task UpdateAsync(Candidate candidate)
        {
            _dbContext.Update(candidate);
            return Task.CompletedTask;
        }

        public async Task SetShortlistAsync(Guid candidateId, bool isShortlisted)
        {
            var existing = await _dbContext.Candidates.FirstOrDefaultAsync(c => c.Id == candidateId);
            if (existing == null) return;
            existing.SetShortlisted(isShortlisted);
            _dbContext.Update(existing);
        }
    }
}
