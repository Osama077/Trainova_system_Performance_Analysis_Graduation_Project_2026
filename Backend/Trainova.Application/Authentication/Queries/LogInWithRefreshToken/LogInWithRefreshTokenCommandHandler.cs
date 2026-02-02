using Trainova.Application.Common.Interfaces.Repositories;
using Trainova.Application.Common.Interfaces.Service;
using Trainova.Application.Authentication.Common;
using MediatR;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Authentication.Queries.LogInWithRefreshToken
{
    public class LogInWithRefreshTokenQueryHandler
        (ITokenGenerator _tokenGenerator,
         IUserTokensRepository _refreshTokenRepository,
         IUnitOfWork _unitOfWork,
         IUserTokensRepository _tokensRepository)
        : IRequestHandler<LogInWithRefreshTokenQuery, ResultOf<AuthenticationResult>>
    {
        public async Task<ResultOf<AuthenticationResult>> Handle(LogInWithRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();


        }
    }
}
