using MediatR;
using Trainova.Application.Authentication.Common;
using Trainova.Application.Common.Interfaces.Repositories.CommonRepos;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.Common.Outbox;
using Trainova.Domain.UserAuth.UserTokens;

#nullable disable
namespace Trainova.Application.Authentication.Commands.CreateToken
{
    public class CreateTokenCommandHandler(
        IUsersRepository _usersRepository,
        IUserTokensRepository _tokenRepsitory,
        IUnitOfWork _unitOfWork,
        IEmailOutboxRepository _emailOutboxRepository,
        CurrentUser _currentUser)
        : IRequestHandler<CreateTokenCommand, ResultOf<BaseTokenCreationResult>>
    {
        public async Task<ResultOf<BaseTokenCreationResult>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _usersRepository.GetByEmailAsync(request.Email);

                if (user is null)
                {
                    return Error.Conflict(
                        code: "CreateTokenCommandHandler.Handle_NotFound",
                        description: "No, Account With this Email.")
                        .AsError<BaseTokenCreationResult>();
                }

                var tokenType = TokenType.FromName(request.Type);


                var token = new UserToken(user.Id, tokenType);


                var email = new EmailOutbox(
                            user.Id,
                            user.ShowName,
                            tokenType != TokenType.PasswordReset ? _currentUser.Email : user.Email,
                            tokenType.Name,
                            token.Token
                            );

                await _unitOfWork.StartTransactionAsync();

                await _tokenRepsitory.AddAsync(token);

                await _emailOutboxRepository.AddAsync(email);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.CommitTransactionAsync();


                BaseTokenCreationResult result = tokenType switch
                {
                    var t when t == TokenType.EmailConfirmation => new ConfirmEmailTokenCreationResult(user.Email),
                    var t when t == TokenType.RefreshToken => new RefreshTokenCreationResult(token.Token),
                    var t when t == TokenType.PasswordReset => new PassWordResetTokenCreationResult(user.Id, user.FullName, user.ShowName, user.PhotoPath),
                    _ => throw new ArgumentOutOfRangeException(nameof(tokenType), $"Type {tokenType} is not supported.")
                };


                return result.AsDone();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(code: ex.Code, description: ex.Message).AsError<BaseTokenCreationResult>();
            }
            catch (Exception ex)
            {
                return Error.Unexpected(code: "CreateToken.Handle_UnexpectedError", description: ex.Message).AsError<BaseTokenCreationResult>();
            }


        }
    }
}
