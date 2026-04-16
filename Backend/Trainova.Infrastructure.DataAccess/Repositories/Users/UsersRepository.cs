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

        public Task AddUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<User?> GetByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }


        public void Update(User user)
        {
            throw new NotImplementedException();
        }


    }
}
