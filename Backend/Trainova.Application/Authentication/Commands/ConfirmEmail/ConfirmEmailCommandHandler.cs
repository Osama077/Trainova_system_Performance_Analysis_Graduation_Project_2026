using Trainova.Application.Common.Interfaces.Services;
using MediatR;
using Trainova.Application.Common.Interfaces.Repositories;
using Trainova.Common.ResultOf;
using Trainova.Domain.Users;
using Trainova.Common.Errors;


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
                    return Error.Custom(code: "NotFound", description: "Some Thing Went Worng", type: 3);
                }

                var user = await _usersRepository.GetByIdAsync(Token.UserId);

                if (user is null
                    || !(user.Id == Token.UserId))
                {
                    return Error.Custom(code: "NotFound", description: "Some Thing Went Worng", type: 3);
                }

                if (Token.IsExpired)
                    return Error.Custom(code: "Expired", description: "The token has expired.", type: 3);

                user.ConfirmEmail();
                Token.MarkUsed();

                await _unitOfWork.StartTransactionAsync();
                _tokenRepsitory.Update(Token);
                _usersRepository.Update(user);
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
