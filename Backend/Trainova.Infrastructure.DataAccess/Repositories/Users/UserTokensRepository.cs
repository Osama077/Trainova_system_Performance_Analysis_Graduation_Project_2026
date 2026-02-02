using Trainova.Application.Common.Interfaces.Repositories;
using Trainova.Domain.Users;

namespace Trainova.Infrastructure.DataAccess.Repositories.Users
{
    public class UserTokensRepository : IUserTokensRepository
    {
        public Task AddAsync(UserToken token)
        {
            throw new NotImplementedException();
        }

        public Task<UserToken?> GetByTokenAndTypeAsync(string token, TokenType type)
        {
            throw new NotImplementedException();
        }

        public void Update(UserToken token)
        {
            throw new NotImplementedException();
        }
    }
}
