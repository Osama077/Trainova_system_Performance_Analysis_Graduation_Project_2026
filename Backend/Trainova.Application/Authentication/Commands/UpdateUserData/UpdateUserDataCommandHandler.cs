using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.ResultOf;
using Trainova.Domain.UserAuth.Users;

namespace Trainova.Application.Authentication.Commands.UpdateUserData
{
    public class UpdateUserDataCommandHandler
        (IUsersRepository _userRepository)
        : IRequestHandler<UpdateUserDataCommand, ResultOf<User>>
        
    {
        public async Task<ResultOf<User>> Handle(UpdateUserDataCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

        }
    }
}
