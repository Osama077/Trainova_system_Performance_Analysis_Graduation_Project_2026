using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Trainova.Application.Common.Interfaces.Repositories.Scouting.Candidates;
using Trainova.Application.Scouting.Candidates;
using Trainova.Domain.Scouting;
using Trainova.Domain.Common.Enums;
using Trainova.Infrastructure.DataAccess;

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
        _dbContext.Update(candidate);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<ScoutingCandidate?> GetByIdAsync(Guid candidateId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ScoutingCandidates.FirstOrDefaultAsync(c => c.Id == candidateId, cancellationToken);
    }

    public async Task<IEnumerable<CandidateListItemResponse>> GetCandidatesAsync(Guid? candidateId = null, string? searchTerm = null, int? mainPositionFilter = null, CandidateStatus? statusFilter = null, Guid? currentTeamId = null, int? minAge = null, int? maxAge = null, DateTime? dateFrom = null, DateTime? dateTo = null, int pageNumber = 0, int pageSize = 12, string sortColumn = "CreatedAt", string sortDirection = "DESC", CancellationToken cancellationToken = default)
    {
        try
        {
            var q = _dbContext.ScoutingCandidates.AsQueryable();

            if (candidateId.HasValue)
                q = q.Where(c => c.Id == candidateId.Value);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var s = searchTerm.Trim();
                q = q.Where(c => c.FullName.Contains(s) || c.CurrentTeamId.ToString().Contains(s));
            }

            if (mainPositionFilter.HasValue)
                q = q.Where(c => (int)c.CurrentMainPosition == mainPositionFilter.Value);

            if (statusFilter.HasValue)
                q = q.Where(c => ((int)c.Status & (int)statusFilter.Value) != 0);

            if (currentTeamId.HasValue)
                q = q.Where(c => c.CurrentTeamId == currentTeamId);

            if (minAge.HasValue)
                q = q.Where(c => c.Age >= minAge.Value);

            if (maxAge.HasValue)
                q = q.Where(c => c.Age <= maxAge.Value);

            q = q.OrderByDescending(c => c.CreatedAt);

            var items = await q.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            // Try to materialize team names if Teams exist in the model
            var teamIds = items.Where(i => i.CurrentTeamId.HasValue).Select(i => i.CurrentTeamId!.Value).Distinct().ToList();
            var teams = new Dictionary<Guid, string>();
            if (teamIds.Any())
            {
                // Teams may live in a different namespace; try to fetch by EF set
                try
                {
                    var teamSet = _dbContext.Set<Trainova.Domain.SeasonsAnalyses.Team>();
                    var teamList = await teamSet.Where(t => teamIds.Contains(t.Id)).ToListAsync(cancellationToken);
                    teams = teamList.ToDictionary(t => t.Id, t => t.TeamName ?? string.Empty);
                }
                catch
                {
                    // ignore if Team entity not mapped; leave team names null
                }
            }

            return items.Select(i => new CandidateListItemResponse
            {
                Id = i.Id,
                FullName = i.FullName,
                Age = i.Age,
                Position = (int)i.CurrentMainPosition,
                CurrentTeamId = i.CurrentTeamId,
                CurrentTeamName = i.CurrentTeamId.HasValue && teams.ContainsKey(i.CurrentTeamId.Value) ? teams[i.CurrentTeamId.Value] : null,
                Nationality = i.Nationality,
                PerformanceScore = i.PerformanceScore,
                ScoutRating = i.ScoutRating,
                PerformanceLevel = i.PerformanceLevel,
                Status = (int)i.Status,
                // skills
                Pace = i.Pace,
                Shooting = i.Shooting,
                Dribbling = i.Dribbling,
                Passing = i.Passing,
                Physicality = i.Physicality,
                Positioning = i.Positioning,
                Defending = i.Defending,
                Vision = i.Vision,

                ShortlistRank = i.ShortlistRank,
                IsOnTrial = ((int)i.Status & (int)CandidateStatus.OnTrial) != 0,
                ContractEnd = i.ContractEnd,
                MarketValue = i.MarketValue,
                Agent = i.Agent,
                MatchesWatchedCount = i.MatchesWatchedCount,

                // Use Notes property stored on the candidate for snippet
                NotesSnippet = i.Notes
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
}