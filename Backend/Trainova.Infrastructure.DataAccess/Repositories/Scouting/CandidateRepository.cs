using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Application.Scouting.Candidates;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.Scouting;

namespace Trainova.Infrastructure.DataAccess.Repositories.Scouting;

public class CandidateRepository : ICandidateRepository
{
    private readonly TrainovaWriteDbContext _dbContext;

    public CandidateRepository(TrainovaWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(ScoutingCandidate candidate, CancellationToken cancellationToken = default)
    {
        // Let any exceptions bubble so callers can handle and return useful details
        await _dbContext.AddAsync(candidate, cancellationToken);
    }

    public Task UpdateAsync(ScoutingCandidate candidate, CancellationToken cancellationToken = default)
    {
        var entry = _dbContext.Entry(candidate);
        if (entry.State == EntityState.Detached)
        {
            // Entity came from outside the current DbContext scope.
            // Attach it and mark as modified, but ensure any genuinely new
            // child entities (those with no matching row in the DB yet) are
            // explicitly marked as Added so EF issues INSERT, not UPDATE.
            _dbContext.Attach(candidate);
            entry.State = EntityState.Modified;

            // Fix child collections: new notes/matches should be Added, not Modified
            foreach (var note in candidate.NotesList)
            {
                var noteEntry = _dbContext.Entry(note);
                if (noteEntry.State == EntityState.Modified)
                    noteEntry.State = EntityState.Added;
            }

            foreach (var match in candidate.MatchesList)
            {
                var matchEntry = _dbContext.Entry(match);
                if (matchEntry.State == EntityState.Modified)
                    matchEntry.State = EntityState.Added;
            }
        }
        // If the entity is already tracked (the normal case after GetByIdAsync),
        // EF change-tracking will automatically detect:
        //   - scalar changes on the candidate → Modified
        //   - new child objects added to collections → Added
        // No explicit call to Update() is needed (and calling it would incorrectly
        // mark new children as Modified, causing a 0-rows-affected concurrency error).
        return Task.CompletedTask;
    }

    public Task DeleteAsync(ScoutingCandidate candidate, CancellationToken cancellationToken = default)
    {
        _dbContext.Remove(candidate);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<ScoutingCandidate?> GetByIdAsync(Guid candidateId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ScoutingCandidates
            .Include(c => c.NotesList)
            .Include(c => c.MatchesList)
            .FirstOrDefaultAsync(c => c.Id == candidateId, cancellationToken);
    }

    public async Task<IEnumerable<CandidateListItemResponse>> GetCandidatesAsync(Guid? candidateId = null, string? searchTerm = null, int? mainPositionFilter = null, CandidateStatus? statusFilter = null, string? currentTeamName = null, int? minAge = null, int? maxAge = null, DateTime? dateFrom = null, DateTime? dateTo = null, int pageNumber = 0, int pageSize = 12, string sortColumn = "CreatedAt", string sortDirection = "DESC", CancellationToken cancellationToken = default)
    {
        try
        {
            var q = _dbContext.ScoutingCandidates.AsQueryable();

            if (candidateId.HasValue)
                q = q.Where(c => c.Id == candidateId.Value);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var s = searchTerm.Trim();
                q = q.Where(c => c.FullName.Contains(s) || (c.CurrentTeamName != null && c.CurrentTeamName.Contains(s)));
            }

            if (mainPositionFilter.HasValue)
                q = q.Where(c => (int)c.Position == mainPositionFilter.Value);

            if (statusFilter.HasValue)
                q = q.Where(c => ((int)c.Status & (int)statusFilter.Value) != 0);

            if (!string.IsNullOrWhiteSpace(currentTeamName))
                q = q.Where(c => c.CurrentTeamName == currentTeamName);

            if (minAge.HasValue)
                q = q.Where(c => c.Age >= minAge.Value);

            if (maxAge.HasValue)
                q = q.Where(c => c.Age <= maxAge.Value);

            var totalCount = await q.CountAsync(cancellationToken);

            q = q.OrderByDescending(c => c.CreatedAt);

            var items = await q.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return items.Select(i => new CandidateListItemResponse
            {
                Id = i.Id,
                FullName = i.FullName,
                Age = i.Age,
                Position = (int)i.Position,
                CurrentTeamName = i.CurrentTeamName,
                Nationality = i.ContractInfo.Nationality,
                ScoutRating = i.ScoutRating,
                Status = (int)i.Status,
                // skills
                Pace = i.SkillAssessment.Pace,
                Shooting = i.SkillAssessment.Shooting,
                Dribbling = i.SkillAssessment.Dribbling,
                Passing = i.SkillAssessment.Passing,
                Physicality = i.SkillAssessment.Physicality,
                Positioning = i.SkillAssessment.Positioning,
                Defending = i.SkillAssessment.Defending,
                Vision = i.SkillAssessment.Vision,

                ShortlistRank = i.ShortlistRank,
                IsOnTrial = ((int)i.Status & (int)CandidateStatus.OnTrial) != 0,
                ContractEnd = i.ContractInfo.ContractEnd,
                MarketValue = i.ContractInfo.MarketValue,
                Agent = i.ContractInfo.Agent,
                MatchesWatchedCount = i.MatchesWatchedCount,

                // Use Notes property stored on the candidate for snippet
                NotesSnippet = i.Notes,
                TotalCount = totalCount
            });
        }
        catch (SqlException sqlEx)
        {
            // Surface helpful details for debugging mapping/column issues
            var message = $"Database error while fetching scouting candidates: {sqlEx.Message}";
            if (sqlEx.Errors != null && sqlEx.Errors.Count > 0)
            {
                var columns = sqlEx.Errors.Cast<SqlError>()
                    .Select(e => e.Message)
                    .Distinct();
                message += "; Details: " + string.Join(" | ", columns);
            }

            throw new InvalidOperationException(message, sqlEx);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error while fetching scouting candidates.", ex);
        }
    }

    public async Task<CandidatesOverviewResponse> GetCandidatesOverviewAsync(Guid? candidateId = null, string? searchTerm = null, int? mainPositionFilter = null, CandidateStatus? statusFilter = null, string? currentTeamName = null, int? minAge = null, int? maxAge = null, DateTime? dateFrom = null, DateTime? dateTo = null, int pageNumber = 0, int pageSize = 12, string sortColumn = "CreatedAt", string sortDirection = "DESC", CancellationToken cancellationToken = default)
    {
        try
        {
            var baseQuery = _dbContext.ScoutingCandidates.AsQueryable();

            if (candidateId.HasValue)
                baseQuery = baseQuery.Where(c => c.Id == candidateId.Value);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var s = searchTerm.Trim();
                baseQuery = baseQuery.Where(c => c.FullName.Contains(s) || (c.CurrentTeamName != null && c.CurrentTeamName.Contains(s)));
            }

            if (mainPositionFilter.HasValue)
                baseQuery = baseQuery.Where(c => (int)c.Position == mainPositionFilter.Value);

            if (statusFilter.HasValue)
                baseQuery = baseQuery.Where(c => ((int)c.Status & (int)statusFilter.Value) != 0);

            if (!string.IsNullOrWhiteSpace(currentTeamName))
                baseQuery = baseQuery.Where(c => c.CurrentTeamName == currentTeamName);

            if (minAge.HasValue)
                baseQuery = baseQuery.Where(c => c.Age >= minAge.Value);

            if (maxAge.HasValue)
                baseQuery = baseQuery.Where(c => c.Age <= maxAge.Value);

            if (dateFrom.HasValue)
                baseQuery = baseQuery.Where(c => c.CreatedAt >= dateFrom.Value);

            if (dateTo.HasValue)
                baseQuery = baseQuery.Where(c => c.CreatedAt <= dateTo.Value);

            // Use AsNoTracking for read-only
            var query = baseQuery.AsNoTracking();

            // Execute count queries sequentially to avoid concurrent use of the same DbContext
            var totalCount = await query.CountAsync(cancellationToken);
            var shortlisted = await query.Where(c => ((int)c.Status & (int)CandidateStatus.Shortlisted) != 0).CountAsync(cancellationToken);
            var signed = await query.Where(c => ((int)c.Status & (int)CandidateStatus.Signed) != 0).CountAsync(cancellationToken);

            // Ordering - simple default by CreatedAt desc
            var itemsQuery = query.OrderByDescending(c => c.CreatedAt)
                                  .Skip(pageNumber * pageSize)
                                  .Take(pageSize);

            var items = await itemsQuery.ToListAsync(cancellationToken);

            var mappedItems = items.Select(i => new CandidateListItemResponse
            {
                Id = i.Id,
                FullName = i.FullName,
                Age = i.Age,
                Position = (int)i.Position,
                CurrentTeamName = i.CurrentTeamName,
                Nationality = i.ContractInfo.Nationality,
                ScoutRating = i.ScoutRating,
                Status = (int)i.Status,
                Pace = i.SkillAssessment.Pace,
                Shooting = i.SkillAssessment.Shooting,
                Dribbling = i.SkillAssessment.Dribbling,
                Passing = i.SkillAssessment.Passing,
                Physicality = i.SkillAssessment.Physicality,
                Positioning = i.SkillAssessment.Positioning,
                Defending = i.SkillAssessment.Defending,
                Vision = i.SkillAssessment.Vision,
                ShortlistRank = i.ShortlistRank,
                IsOnTrial = ((int)i.Status & (int)CandidateStatus.OnTrial) != 0,
                ContractEnd = i.ContractInfo.ContractEnd,
                MarketValue = i.ContractInfo.MarketValue,
                Agent = i.ContractInfo.Agent,
                MatchesWatchedCount = i.MatchesWatchedCount,
                NotesSnippet = i.Notes
            }).ToList();

            return new CandidatesOverviewResponse
            {
                Counts = new OverviewCounts
                {
                    TotalCandidates = totalCount,
                    Shortlisted = shortlisted,
                    PlayersSigned = signed
                },
                Items = mappedItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
        catch (SqlException sqlEx)
        {
            var message = $"Database error while fetching scouting candidates overview: {sqlEx.Message}";
            if (sqlEx.Errors != null && sqlEx.Errors.Count > 0)
            {
                var columns = sqlEx.Errors.Cast<SqlError>()
                    .Select(e => e.Message)
                    .Distinct();
                message += "; Details: " + string.Join(" | ", columns);
            }

            throw new InvalidOperationException(message, sqlEx);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error while fetching scouting candidates overview.", ex);
        }
    }
}