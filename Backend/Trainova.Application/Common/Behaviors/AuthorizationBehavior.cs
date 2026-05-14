using System.Reflection;
using Trainova.Application.Common.Authorization;
using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Common.Errors;
using Trainova.Application.Common.Models;
using Trainova.Domain.UserAuth;

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
            .ToHashSet();

        if (authorizationAttributes.Count == 0)
        {
            return await next();
        }

        if (_currentUser == null || !_currentUser.IsAuthenticated)
        {
            return (dynamic)Error.Unauthorized(description: "User is not loged Id");
        }


        if (request is IPlayerAuthraizedRequest playerAuthraizedRequest)
            MatchPlayerId(playerAuthraizedRequest);

        if (_currentUser.IsAuthenticated
            && (_currentUser.Role.Contains(Role.SystemAdmin.Name) || _currentUser.Role.Contains(Role.SystemOwner.Name)))
            return await next();


        var requiredRoles = authorizationAttributes
            .SelectMany(authorizationAttribute => authorizationAttribute.Roles?.Split(',') ?? [])
            .ToList();

        if (!requiredRoles.Any() || requiredRoles.Any(r => _currentUser.Role == r))
            return await next();


        return (dynamic)Error.Unauthorized(description: "User is forbidden from taking this action");
    }

    private void MatchPlayerId(IPlayerAuthraizedRequest playerAuthraizedRequest)
    {
        if(_currentUser.Role==Role.Player.Name)
            playerAuthraizedRequest.PlayerId = _currentUser.Id;
    }

}

