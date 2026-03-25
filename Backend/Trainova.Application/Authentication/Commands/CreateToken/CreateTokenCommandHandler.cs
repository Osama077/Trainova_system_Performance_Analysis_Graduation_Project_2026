using Trainova.Application.Common.Interfaces.Service;
using MediatR;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using Trainova.Application.Common.Helpers;
using Trainova.Domain.UserAuth.UserTokens;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;


namespace Trainova.Application.Authentication.Commands.CreateToken
{
    public class CreateTokenCommandHandler(
        IUsersRepository _usersRepository,
        ITokenGenerator _tokenGenerator,
        IUserTokensRepository _tokenRepsitory,
        IEmailSender _emailService)
        : IRequestHandler<CreateTokenCommand, ResultOf<Done>>
    {
        public async Task<ResultOf<Done>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {


            var user = await _usersRepository.GetByEmailAsync(request.Email);

            if (user is null)
            {
                return Error.Conflict(code: "CreateTokenCommandHandler.Handle_NotFound", description: "No, Account With this Email.");
            }

            var tokenType = TokenType.FromName(request.Type);


            var token =  _tokenGenerator.GenerateUserTokens(user,tokenType);

            

            await _tokenRepsitory.AddAsync(token);
            ///Bad form 
            await _emailService.SendEmailAsync(
                request.Email,
                $"Your {request.Type} Token From Trainova",
                EmailBodyTemplates.GenerateTemplate(user.Email, token.Token, token.TokenType)
            );

            return Done.NoContent.AsNoContent();

        }
    }
}
