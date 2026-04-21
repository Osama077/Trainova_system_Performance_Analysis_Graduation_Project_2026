using System.Reflection;
using Trainova.Application.Common.Authorization;
using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using Trainova.Application.Common.Models;

namespace Trainova.Application.Common.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse>(CurrentUser? _currentUser)
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

        if (_currentUser == null)
        {
            return (dynamic)Error.Unauthorized(description: "User is not loged Id");
        }
        if (_currentUser.IsAuthenticated && _currentUser.Roles.Contains("SystemOwner"))
        {
            return await next();
        }


        var requiredRoles = authorizationAttributes
            .SelectMany(authorizationAttribute => authorizationAttribute.Role?.Split(',') ?? [])
            .ToList();

        if (!requiredRoles.Intersect(_currentUser.Roles).Any())
        {
            return (dynamic)Error.Unauthorized(description: "User is forbidden from taking this action");
        }


        return await next();
    }

}

