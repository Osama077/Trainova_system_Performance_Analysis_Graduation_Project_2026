using Trainova.Application.Common.Interfaces.Services;
using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using Trainova.Domain.UserAuth.UserTokens;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Application.Common.Models;
using Trainova.Domain.Common.Helpers;


namespace Trainova.Application.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler(
        IUsersRepository _usersRepository,
        IUnitOfWork _unitOfWork,
        IUserTokensRepository _tokenRepsitory,
        CurrentUser _currentUser)
        : IRequestHandler<ConfirmEmailCommand, ResultOf<Done>>


    {
        public async Task<ResultOf<Done>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var Token = await _tokenRepsitory.GetTokenAsync(
                    request.Token,
                    TokenType.EmailConfirmation,
                    _currentUser.Id);


                if(Token is null)
                {
                    return Error.NotFound(code: "ConfirmEmailCommandHandler.Handle_NotFound", description: "token is null");
                }

                var user = await _usersRepository.GetByEmailAsync(_currentUser!.Email);

                if (user is null)
                {
                    return Error.NotFound(code: "ConfirmEmailCommandHandler.Handle_NotFound", description: "user is null");
                }

                if (user.Id != Token.UserId)
                {
                    Token.Revoke(RevokeCause.UserNotMatch);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    return Error.Conflict(
                        code: "ConfirmEmailCommandHandler.Handle_Conflict",
                        description: "the user email cant match the the user Id related to the tokens");
                }
                if (Token.IsExpired)
                    return Error.Conflict(code: "Expired", description: "The token has expired.");

                user.ConfirmEmail();
                Token.MarkUsed();

                await _unitOfWork.StartTransactionAsync();

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.CommitTransactionAsync();


                return Done.done.AsNoContent();

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
