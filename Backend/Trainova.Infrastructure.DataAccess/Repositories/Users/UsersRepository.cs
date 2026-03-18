using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Domain.UserAuth.Users;

namespace Trainova.Infrastructure.DataAccess.Repositories.Users
{
    public class UsersRepository : IUsersRepository
    {
        public Task AddUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
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
