﻿using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions;

namespace Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity
{
    public interface ITrainingSessionRepository
    {
        Task AddAsync(TrainingSession session);
        Task<TrainingSession?> GetByIdAsync(Guid id);
        Task UpdateAsync(TrainingSession session);
        Task DeleteAsync(TrainingSession session);
        Task<bool> ExistsAsync(Guid? planId = null, Guid? accessPolicyId = null);
    }
}
