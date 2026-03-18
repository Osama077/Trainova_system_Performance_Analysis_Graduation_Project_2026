using System.Data;
using Trainova.Domain.UserAuth.UserTokens;


namespace Trainova.Application.Common.Interfaces.Repositories.UserAuth
{
    public interface IUserTokensRepository
    {
        public Task AddAsync(UserToken token);
        public Task<UserToken?> GetByTokenAndTypeAsync(string token, TokenType type);
        public void Update(UserToken token);
    }
}
