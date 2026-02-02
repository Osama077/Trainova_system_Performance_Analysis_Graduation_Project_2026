using System.Data;
using Trainova.Domain.Users;


namespace Trainova.Application.Common.Interfaces.Repositories
{
    public interface IUserTokensRepository
    {
        public Task AddAsync(UserToken token);
        public Task<UserToken?> GetByTokenAndTypeAsync(string token, TokenType type);
        public void Update(UserToken token);
    }
}
