﻿using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Infrastructure.DataAccess.DbSettingsObjects;
using Microsoft.EntityFrameworkCore;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Infrastructure.DataAccess.Repositories.TrainingSessionAccessablity
{
    internal class AccsessPolicyRepository : IAccessPolicyRepository
    {
        public AccsessPolicyRepository(IDbSettings dbSettings, TrainovaWriteDbContext dbContext)
        {
            _dbSettings = dbSettings;
            _dbContext = dbContext;
        }
        private readonly TrainovaWriteDbContext _dbContext;
        private readonly IDbSettings _dbSettings;

        public async Task AddAsync(AccessPolicy accessPolicy)
        {
            await _dbContext.AccessPolicies.AddAsync(accessPolicy);
        }

        public async Task<AccessPolicy?> GetByIdAsync(Guid value)
        {
            return await _dbContext.AccessPolicies.FirstOrDefaultAsync(x => x.Id == value);
        }

        public async Task UpdateAsync(AccessPolicy accessPolicy)
        {
            _dbContext.AccessPolicies.Update(accessPolicy);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(AccessPolicy accessPolicy)
        {
            _dbContext.AccessPolicies.Remove(accessPolicy);
            await Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Guid? id = null)
        {
            var query = _dbContext.AccessPolicies.AsQueryable();

            if (id.HasValue)
                query = query.Where(x => x.Id == id);

            return await query.AnyAsync();
        }

        public async Task<IEnumerable<AccessPolicy>> SearchAsync(string? searchTerm = null, int pageNumber = 0, int pageSize = 25, string? sortColumn = null, string? sortDirection = null)
        {
            var query = _dbContext.AccessPolicies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim();
                query = query.Where(x => x.PolicyName != null && x.PolicyName.Contains(term));
            }

            // simple sorting - default by CreatedAt desc
            if (string.IsNullOrWhiteSpace(sortColumn))
            {
                query = query.OrderByDescending(x => x.CreatedAt);
            }
            else
            {
                // only a very small whitelist to avoid SQL injection via raw column names
                if (string.Equals(sortColumn, "PolicyName", StringComparison.OrdinalIgnoreCase))
                    query = sortDirection?.ToUpper() == "ASC" ? query.OrderBy(x => x.PolicyName) : query.OrderByDescending(x => x.PolicyName);
                else if (string.Equals(sortColumn, "CreatedAt", StringComparison.OrdinalIgnoreCase))
                    query = sortDirection?.ToUpper() == "ASC" ? query.OrderBy(x => x.CreatedAt) : query.OrderByDescending(x => x.CreatedAt);
                else
                    query = query.OrderByDescending(x => x.CreatedAt);
            }

            if (pageNumber < 0) pageNumber = 0;
            if (pageSize <= 0) pageSize = 25;

            query = query.Skip(pageNumber * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Common.AccessPolicyReadModel>> SearchWithUsageAsync(string? searchTerm = null, string? usageType = null, int pageNumber = 0, int pageSize = 25, string? sortColumn = null, string? sortDirection = null)
        {
            // First get the paginated policies using the existing logic
            var query = _dbContext.AccessPolicies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim();
                query = query.Where(x => x.PolicyName != null && x.PolicyName.Contains(term));
            }

            // apply usageType filter at query level if provided
            if (!string.IsNullOrWhiteSpace(usageType))
            {
                var ut = usageType.Trim();
                if (string.Equals(ut, "Plan", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(p => _dbContext.Plans.Any(pl => pl.AccessPolicyId == p.Id));
                }
                else if (string.Equals(ut, "TrainingSession", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(p => _dbContext.TrainingSessions.Any(ts => ts.AccessPolicyId == p.Id));
                }
                else if (string.Equals(ut, "Both", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(p => _dbContext.Plans.Any(pl => pl.AccessPolicyId == p.Id) && _dbContext.TrainingSessions.Any(ts => ts.AccessPolicyId == p.Id));
                }
                else if (string.Equals(ut, "Unused", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(p => !_dbContext.Plans.Any(pl => pl.AccessPolicyId == p.Id) && !_dbContext.TrainingSessions.Any(ts => ts.AccessPolicyId == p.Id));
                }
            }

            if (string.IsNullOrWhiteSpace(sortColumn))
            {
                query = query.OrderByDescending(x => x.CreatedAt);
            }
            else
            {
                if (string.Equals(sortColumn, "PolicyName", StringComparison.OrdinalIgnoreCase))
                    query = sortDirection?.ToUpper() == "ASC" ? query.OrderBy(x => x.PolicyName) : query.OrderByDescending(x => x.PolicyName);
                else if (string.Equals(sortColumn, "CreatedAt", StringComparison.OrdinalIgnoreCase))
                    query = sortDirection?.ToUpper() == "ASC" ? query.OrderBy(x => x.CreatedAt) : query.OrderByDescending(x => x.CreatedAt);
                else
                    query = query.OrderByDescending(x => x.CreatedAt);
            }

            if (pageNumber < 0) pageNumber = 0;
            if (pageSize <= 0) pageSize = 25;

            // compute total count before pagination
            var totalCount = await query.CountAsync();

            var policies = await query.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();

            // collect ids to check usage efficiently
            var policyIds = policies.Select(p => p.Id).ToList();

            var planPolicyIds = await _dbContext.Plans.Where(pl => policyIds.Contains(pl.AccessPolicyId)).Select(pl => pl.AccessPolicyId).Distinct().ToListAsync();
            var sessionPolicyIds = await _dbContext.TrainingSessions.Where(ts => policyIds.Contains(ts.AccessPolicyId)).Select(ts => ts.AccessPolicyId).Distinct().ToListAsync();

            var result = policies.Select(p => new Trainova.Application.TrainingSessionsAccessibility.AccessPolicies.Common.AccessPolicyReadModel
            {
                Id = p.Id,
                PolicyName = p.PolicyName,
                UsedInPlans = planPolicyIds.Contains(p.Id),
                UsedInTrainingSessions = sessionPolicyIds.Contains(p.Id),
                CreatedAt = p.CreatedAt, // Map CreatedAt to the ReadModel
                TotalCount = totalCount
            }).ToList();

            return result;
        }
    }
}
