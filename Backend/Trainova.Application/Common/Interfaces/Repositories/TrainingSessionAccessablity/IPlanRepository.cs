using System;
using System.Collections.Generic;
using System.Text;

namespace Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity
{
    public interface IPlanRepository
    {
        Task<bool> ExistsAsync(Guid value);
    }
}
