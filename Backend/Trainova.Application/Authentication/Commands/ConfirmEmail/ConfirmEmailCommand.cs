using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Authentication.Commands.ConfirmEmail;

[Authorize]
public record ConfirmEmailCommand (string Token) : IRequest<ResultOf<Done>>;

