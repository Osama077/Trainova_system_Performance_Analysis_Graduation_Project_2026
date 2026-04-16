using MediatR;
using Trainova.Application.Authentication.Common;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Authentication.Commands.CreateToken;
public record CreateTokenCommand(string Type, string? Email = null) : IRequest<ResultOf<BaseTokenCreationResult>>;




