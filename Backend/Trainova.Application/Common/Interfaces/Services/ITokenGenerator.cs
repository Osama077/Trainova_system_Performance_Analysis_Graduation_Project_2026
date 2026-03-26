using Trainova.Domain.UserAuth.Roles;
using Trainova.Domain.UserAuth.Users;

namespace Trainova.Application.Common.Interfaces.Services;

public interface ITokenGenerator
{
    string GenerateJwtToken(User? user,List<Role>? roles=null);

}