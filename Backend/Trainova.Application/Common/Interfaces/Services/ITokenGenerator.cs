using Trainova.Domain.UserAuth;

namespace Trainova.Application.Common.Interfaces.Services;

public interface ITokenGenerator
{
    string GenerateJwtToken(User? user);

}