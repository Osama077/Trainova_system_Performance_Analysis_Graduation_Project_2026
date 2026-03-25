using Trainova.Application.Common.Interfaces.Services;
using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using Trainova.Domain.UserAuth.UserTokens;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;


namespace Trainova.Application.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler(
        IUsersRepository _usersRepository,
        IUnitOfWork _unitOfWork,
        IUserTokensRepository _tokenRepsitory)
        : IRequestHandler<ConfirmEmailCommand, ResultOf<Done>>


    {
        public async Task<ResultOf<Done>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var Token = await _tokenRepsitory.GetByTokenAndTypeAsync(request.Token, TokenType.EmailConfirmation);
                if(Token is null)
                {
                    return Error.NotFound(code: "NotFound", description: "token is null");
                }

                var user = await _usersRepository.GetByEmailAsync(request.Email);

                if (user is null)
                {
                    return Error.NotFound(code: "NotFound", description: "user is null");
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

                _tokenRepsitory.Update(Token);
                _usersRepository.Update(user);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();


                return Done.done.AsNoContent();

            }
            catch (Exception ex)
            {
                return Error.Unexpected(code: "ConfirmEmailUnexpectedError", description: ex.Message);
            }
        }
    }
}
