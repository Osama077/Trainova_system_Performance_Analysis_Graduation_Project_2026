using MediatR;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Profiles.Commands.UpdatePlayerProfile;

public class UpdatePlayerProfileCommandHandler : IRequestHandler<UpdatePlayerProfileCommand, ResultOf<object>>
{
    public async Task<ResultOf<object>> Handle(UpdatePlayerProfileCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
