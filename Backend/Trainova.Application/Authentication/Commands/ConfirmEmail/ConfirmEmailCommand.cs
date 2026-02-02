using MediatR;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Authentication.Commands.ConfirmEmail;

public record ConfirmEmailCommand (string Email,string Token,Guid UserId) : IRequest<ResultOf<Done>>;

