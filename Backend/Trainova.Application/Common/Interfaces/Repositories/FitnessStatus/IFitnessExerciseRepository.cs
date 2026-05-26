using Trainova.Domain.FitnessStatus;

namespace Trainova.Application.Common.Interfaces.Repositories.FitnessStatus
{
    public interface IFitnessExerciseRepository
    {
        Task AddAsync(FitnessExercise exercise);
        Task DeleteAsync(FitnessExercise exercise);
        Task<FitnessExercise> GetByIdAsync(Guid id);
        Task UpdateAsync(FitnessExercise exercise);
    }
}
