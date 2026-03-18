using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Domain.UserAuth.Users;

namespace Trainova.Application.Authentication.Commands.UpdateUserData;

public record UpdateUserDataCommand
    (Guid Id,
    string? ShowName,
    string? FullName,
    string? PhotoPath,
    string? Email): IRequest<ResultOf<User>>;
