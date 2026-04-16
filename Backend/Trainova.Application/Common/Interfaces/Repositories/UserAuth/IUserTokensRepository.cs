using System.Data;
using Trainova.Domain.UserAuth.UserTokens;


namespace Trainova.Application.Common.Interfaces.Repositories.UserAuth
{
    public interface IUserTokensRepository
    {
        public Task AddAsync(UserToken token);
        public Task<UserToken?> GetTokenAsync(string token, TokenType type,Guid? userId = null);
        public void Update(UserToken token);
    }
}
