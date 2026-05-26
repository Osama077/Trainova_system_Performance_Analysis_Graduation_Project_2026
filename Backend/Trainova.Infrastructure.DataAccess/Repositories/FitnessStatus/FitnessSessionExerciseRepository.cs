using Microsoft.EntityFrameworkCore;
using Trainova.Application.Common.Interfaces.Repositories.FitnessStatus;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Infrastructure.DataAccess.Repositories.FitnessStatus
{
    public class FitnessSessionExerciseRepository : IFitnessSessionExerciseRepository
    {
        private readonly TrainovaWriteDbContext _dbContext;

        public FitnessSessionExerciseRepository(TrainovaWriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(FitnessSessionExercise exercise)
        {
            await _dbContext.FitnessSessionExercises.AddAsync(exercise);
        }

        public async Task DeleteAsync(FitnessSessionExercise exercise)
        {
            _dbContext.FitnessSessionExercises.Remove(exercise);
            await Task.CompletedTask;
        }

        public async Task<FitnessSessionExercise?> GetByIdAsync(Guid id)
        {
            return await _dbContext.FitnessSessionExercises
                .Include(fse => fse.Exercise)
                .FirstOrDefaultAsync(fse => fse.Id == id);
        }

        public async Task UpdateAsync(FitnessSessionExercise exercise)
        {
            _dbContext.FitnessSessionExercises.Update(exercise);
            await Task.CompletedTask;
        }

        public async Task<FitnessSessionExercise?> GetBySessionAndExerciseIdAsync(Guid sessionId, Guid exerciseId)
        {
            return await _dbContext.FitnessSessionExercises
                .Include(fse => fse.Exercise)
                .FirstOrDefaultAsync(fse => fse.SessionId == sessionId && fse.ExerciseId == exerciseId);
        }
    }
}
