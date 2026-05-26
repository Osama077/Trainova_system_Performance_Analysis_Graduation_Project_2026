using Trainova.Domain.FitnessStatus;

namespace Trainova.Application.Common.Interfaces.Repositories.FitnessStatus
{
    public interface IFitnessSessionExerciseRepository
    {
        Task AddAsync(FitnessSessionExercise exercise);
        Task DeleteAsync(FitnessSessionExercise exercise);
        Task<FitnessSessionExercise?> GetByIdAsync(Guid id);
        Task UpdateAsync(FitnessSessionExercise exercise);
        Task<FitnessSessionExercise?> GetBySessionAndExerciseIdAsync(Guid sessionId, Guid exerciseId);
    }
}
