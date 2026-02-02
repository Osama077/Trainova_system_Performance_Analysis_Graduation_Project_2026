using System.Reflection;
using Trainova.Application.Common.Authorization;
using MediatR;
using Trainova.Application.Common.Interfaces.Service;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;

namespace Trainova.Application.Common.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse>(ICurrentUserProvider _currentUserProvider)
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IResultOf
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var authorizationAttributes = request.GetType()
            .GetCustomAttributes<AuthorizeAttribute>()
            .ToList();

        if (authorizationAttributes.Count == 0)
        {
            return await next();
        }

        var currentUserRuselt = _currentUserProvider.GetCurrentUser();
        if (currentUserRuselt.IsFailure)
        {
            return (dynamic)currentUserRuselt.Errors.First();
        }
        var currentUser = currentUserRuselt.Value;

        var requiredRoles = authorizationAttributes
            .SelectMany(authorizationAttribute => authorizationAttribute.Role?.Split(',') ?? [])
            .ToList();

        if (requiredRoles.Except(currentUser.Roles).Any())
        {
            return (dynamic)Error.Unauthorized(description: "User is forbidden from taking this action");
        }


        return await next();
    }

}

