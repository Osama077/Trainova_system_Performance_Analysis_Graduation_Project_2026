using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Trainova.Application.Common.Interfaces.Repositories.CommonRepos;
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
            return await _dbContext.EmailOutboxes.Select(
                e => new PendingEmail
                {
                    Id = e.Id,
                    UserEmail = e.UserEmail,
                    UserId = e.UserId,
                    UserName = e.UserName,
                    Token = e.Token,
                    RetryCount = e.RetryCount,
                    IsSent = e.IsSent,
                    EmailType = e.EmailType,
                    CreatedAt = e.CreatedAt,
                })
                .Where(p => !p.IsSent && p.RetryCount <6)
                .Take(take)
                .OrderByDescending(p=>p.CreatedAt)
                .ToListAsync();
        }




        // Obsolete
        //public async Task<IEnumerable<PendingEmail>> GetPendingAsync(int take = 50)
        //{
        //    using var _dbConnection = _dbSettings.CreateReadingConnection();
        //    return await _dbConnection.QueryAsync<PendingEmail>(
        //        "[Reading.UserData].[GetPendingEmails]",
        //        new { Take = take },
        //        commandType: CommandType.StoredProcedure
        //    );
        //}




        public Task UpdateAsync(EmailOutbox email)
        {
            _dbContext.EmailOutboxes.Update(email);
            return Task.CompletedTask;
        }



    }
}
