using Trainova.Application.Authentication.Common;
using MediatR;
using Trainova.Common.ResultOf;
namespace Trainova.Application.Authentication.Queries.LogInWithRefreshToken;
public record LogInWithRefreshTokenQuery(
    string RefreshToken,Guid UserId) : IRequest<ResultOf<AuthenticationResult>>;

