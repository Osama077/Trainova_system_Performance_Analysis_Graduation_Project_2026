﻿using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Domain.TrainingSessionsAccessibility.Plans;

namespace Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity
{
    public interface IPlanRepository
    {
        Task AddAsync(Plan plan);
        Task<Plan?> GetByIdAsync(Guid id);
        Task UpdateAsync(Plan plan);
        Task DeleteAsync(Plan plan);
        Task<bool> ExistsAsync(Guid? id = null, Guid? accessPolicyId = null);
    }
}
