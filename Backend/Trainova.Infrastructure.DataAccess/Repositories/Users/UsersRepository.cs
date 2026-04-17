using Microsoft.EntityFrameworkCore;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Domain.UserAuth.Users;
using Trainova.Infrastructure.DataAccess.DbSettingsObjects;

namespace Trainova.Infrastructure.DataAccess.Repositories.Users
{
    public class UsersRepository : IUsersRepository
    {
        public readonly TrainovaWriteDbContext _dbContext;
        private readonly IDbSettings _dbSettings;

        public UsersRepository(TrainovaWriteDbContext dbContext, IDbSettings dbSettings)
        {
            _dbContext = dbContext;
            _dbSettings = dbSettings;
        }

        public async Task AddUserAsync(User user)
        {
            await _dbContext.AddAsync(user);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<User?> GetByIdAsync(Guid userId)
        {
            return _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }


        public Task UpdateAsync(User user)
        {
            _dbContext.Update(user);
            return Task.CompletedTask;
        }
    }
}
