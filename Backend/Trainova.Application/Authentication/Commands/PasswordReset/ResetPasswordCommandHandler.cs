using MediatR;
using Microsoft.AspNetCore.Http;
using Trainova.Application.Authentication.Common;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.Common.Services;
using Trainova.Domain.UserAuth;

namespace Trainova.Application.Authentication.Commands.PasswordReset
{
    public class ResetPasswordCommandHandler (
        IUsersRepository _usersRepository,
        ITokenGenerator _tokenGenerator,
        IUserTokensRepository _tokenRepsitory,
        IPasswordHasher _passwordHasher,
        IUnitOfWork _unitOfWork,
        IHttpContextAccessor _contextAccessor
        ) : IRequestHandler<ResetPasswordCommand, ResultOf<FullAuthenticationResult>>
    {
        public async Task<ResultOf<FullAuthenticationResult>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _usersRepository.GetByEmailAsync( request.Email );
                if (user is null)
                {
                    return Error.NotFound(code: "UserNotFound", description: $"No user found with email {request.Email}");
                }
                var token = await _tokenRepsitory.GetTokenAsync(request.Token,TokenType.PasswordReset, user.Id);
                if (token is null)
                {
                    return Error.NotFound(code: "UserTokenNotFound", description: $"No Token found with this data {request.Email}");
                }
                token.MarkUsed();


                var userPasswordHashResuelt = user.SetNewPassword(request.NewPassword, _passwordHasher);

                if (userPasswordHashResuelt.IsFailure)
                {
                    return userPasswordHashResuelt.Errors;
                }



                var jwt = _tokenGenerator.GenerateJwtToken(user);

                _contextAccessor.HttpContext.Response.Cookies.Append("access_token", jwt);


                await _unitOfWork.StartTransactionAsync();
                await _tokenRepsitory.UpdateAsync(token);
                await _usersRepository.UpdateAsync(user);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.CommitTransactionAsync();

                return new FullAuthenticationResult(user,jwt);

            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Unexpected(code: "ConfirmEmailUnexpectedError", description: ex.Message);
            }


        }
    }
}
