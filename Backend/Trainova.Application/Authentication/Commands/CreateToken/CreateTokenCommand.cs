using MediatR;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Authentication.Commands.CreateToken;
    public record CreateTokenCommand(string Email, string Type) : IRequest<ResultOf<Done>>;


