using Trainova.Domain.UserAuth;

namespace Trainova.Application.Common.Interfaces.Services;

public interface ITokenGenerator
{
    string GenerateUserJwtToken(User? user);
    string GenerateDeviceJwtToken(Device device);


}