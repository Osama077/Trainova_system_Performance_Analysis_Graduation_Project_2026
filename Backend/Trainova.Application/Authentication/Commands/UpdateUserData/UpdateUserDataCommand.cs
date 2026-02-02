using Trainova.Domain.Users;
using MediatR;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Authentication.Commands.UpdateUserData;

public record UpdateUserDataCommand
    (Guid Id,
    string? ShowName,
    string? FullName,
    string? PhotoPath,
    string? Email): IRequest<ResultOf<User>>;
