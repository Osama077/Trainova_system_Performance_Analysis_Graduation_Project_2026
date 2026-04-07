using Dapper;
using System.Data;
using Trainova.Application.Common.Interfaces.Repositories.CommonRepos;
using Trainova.Application.Common.Models;
using Trainova.Domain.Common.Outbox;
using Trainova.Infrastructure.DataAccess.DbSettingsObjects;

namespace Trainova.Infrastructure.DataAccess.Repositories.Common
{
    public class EmailOutboxRepository : IEmailOutboxRepository
    {
        private readonly IDbSettings _dbSettings;
        private readonly TrainovaWriteDbContext _dbContext;

        public EmailOutboxRepository(IDbSettings dbSettings, TrainovaWriteDbContext dbContext)
        {
            _dbSettings = dbSettings;
            _dbContext = dbContext;
        }


        public async Task AddAsync(EmailOutbox email)
        {
            await _dbContext.EmailOutboxes.AddAsync(email);
        }

        public async Task<IEnumerable<PendingEmail>> GetPendingAsync(int take = 50)
        {
            using var _dbConnection = _dbSettings.CreateReadingConnection();
            return await _dbConnection.QueryAsync<PendingEmail>(
                "[Reading.UserData].[GetPendingEmails]",
                new { Take = take },
                commandType: CommandType.StoredProcedure
            );
        }




        public void UpdateAsync(EmailOutbox email)
        {
            _dbContext.EmailOutboxes.Update(email);
        }

        Task<IEnumerable<PendingEmail>> IEmailOutboxRepository.GetPendingAsync(int take)
        {
            throw new NotImplementedException();
        }
    }
}
