using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Domain.MedicalStatus;

namespace Trainova.Application.Common.Interfaces.Repositories.MedicalStatus
{
    public interface IRecoveryPlanPhasesRepository
    {
        Task AddAsync(RecoveryPlanPhase phase);
        Task<IEnumerable<RecoveryPlanPhase>> GetByPlayerInjuryIdAsync(Guid playerInjuryId);
    }
}
