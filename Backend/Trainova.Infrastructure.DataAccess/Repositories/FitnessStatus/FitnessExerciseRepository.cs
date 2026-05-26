using Microsoft.EntityFrameworkCore;
using Trainova.Application.Common.Interfaces.Repositories.FitnessStatus;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Infrastructure.DataAccess.Repositories.FitnessStatus
{
    public class FitnessExerciseRepository : IFitnessExerciseRepository
    {
        private readonly TrainovaWriteDbContext _dbContext;

        public FitnessExerciseRepository(TrainovaWriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task AddAsync(FitnessExercise exercise)
        {
            await _dbContext.FitnessExercises.AddAsync(exercise);
        }

        public async Task DeleteAsync(FitnessExercise exercise)
        {
            _dbContext.FitnessExercises.Remove(exercise);
            await Task.CompletedTask;
        }

        public async Task<FitnessExercise> GetByIdAsync(Guid id)
        {
            return await _dbContext.FitnessExercises.FirstOrDefaultAsync(e=>e.Id==id);
        }

        public async Task UpdateAsync(FitnessExercise exercise)
        {
            _dbContext.FitnessExercises.Update(exercise);
            await Task.CompletedTask;
        }
    }
}
