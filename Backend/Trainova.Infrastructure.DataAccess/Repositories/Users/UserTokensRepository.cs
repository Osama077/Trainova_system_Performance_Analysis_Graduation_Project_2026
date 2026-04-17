using Microsoft.EntityFrameworkCore;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Domain.UserAuth.UserTokens;

namespace Trainova.Infrastructure.DataAccess.Repositories.Users
{
    public class UserTokensRepository : IUserTokensRepository
    {
        private readonly TrainovaWriteDbContext _context;

        public UserTokensRepository(TrainovaWriteDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserToken token)
        {
            await _context.AddAsync(token);
        }

        public async Task<UserToken?> GetTokenAsync(string token, TokenType type,Guid? userId =null)
        {
            return await _context.UserTokens
                .Where(ut => ut.Token == token && ut.TokenType == type && (ut.UserId == userId || !userId.HasValue))
                .FirstOrDefaultAsync();
        }

        public Task UpdateAsync(UserToken token)
        {
            _context.Update(token);
            return Task.CompletedTask;
        }
    }
}
