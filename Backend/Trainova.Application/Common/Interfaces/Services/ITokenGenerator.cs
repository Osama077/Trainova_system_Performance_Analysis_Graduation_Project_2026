using Trainova.Application.Authentication.Common;
using Trainova.Domain.Users;

namespace Trainova.Application.Common.Interfaces.Service;

public interface ITokenGenerator
{
    UserToken GenerateUserTokens(User? user, TokenType type);
    string GenerateJwtToken(User? user,List<Role>? roles=null);

}