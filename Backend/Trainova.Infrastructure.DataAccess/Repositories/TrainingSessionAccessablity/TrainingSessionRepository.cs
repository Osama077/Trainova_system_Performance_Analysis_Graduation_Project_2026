using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions;
using Trainova.Infrastructure.DataAccess.DbSettingsObjects;

namespace Trainova.Infrastructure.DataAccess.Repositories.TrainingSessionAccessablity
{
    public class TrainingSessionRepository: ITrainingSessionRepository
    {
        public TrainingSessionRepository(IDbSettings dbSettings, TrainovaWriteDbContext dbContext)
        {
            _dbSettings = dbSettings;
            _dbContext = dbContext;
        }
        private readonly TrainovaWriteDbContext _dbContext;
        private readonly IDbSettings _dbSettings;

        public Task AddAsync(TrainingSession session)
        {
            throw new NotImplementedException();
        }
    }
}
