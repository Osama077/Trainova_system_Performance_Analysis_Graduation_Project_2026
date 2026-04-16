using MediatR;
using Trainova.Application.Authentication.Common;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FullName,
    string ShowName,
    string Email,
    string Password) : IRequest<ResultOf<FullAuthenticationResult>>;