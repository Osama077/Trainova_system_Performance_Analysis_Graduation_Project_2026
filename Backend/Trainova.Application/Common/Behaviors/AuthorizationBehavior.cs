using MediatR;
using System.Reflection;
using Trainova.Application.Common.Authorization;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.BaseEntity;
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

        if (request is IPhysicalMachineAuthraizedRequest multiWayCreatableRequest)
        {
            MapCreatorType(multiWayCreatableRequest);
        }

        if (request is IPlayerAuthraizedRequest playerAuthraizedRequest)
            MatchPlayerId(playerAuthraizedRequest);

        if (request is ICreatorAuthraizedRequest creatorAuthraizedRequest)
            MatchCreatorId(creatorAuthraizedRequest);



        if (_currentUser.IsAuthenticated
            && (_currentUser.Role == UserRole.SystemAdmin.Name || _currentUser.Role == UserRole.SystemOwner.Name))
            return await next();


        var requiredRoles = authorizationAttributes
            .SelectMany(authorizationAttribute => authorizationAttribute.Roles?.Split(',') ?? [])
            .ToList();

        if (!requiredRoles.Any() || requiredRoles.Any(r => _currentUser.Role == r))
            return await next();


        return (dynamic)Error.Unauthorized(description: "User is forbidden from taking this action");
    }

    private void MapCreatorType(IPhysicalMachineAuthraizedRequest multiWayCreatableRequest)
    {
        switch (_currentUser.UserType)
        {
            case CurrentUserType.SmartWatch:
            case CurrentUserType.FitnessTracingDevice:
                multiWayCreatableRequest.CreationType = CreationType.FromDevice;
                break;
            case CurrentUserType.MlModelService:
                multiWayCreatableRequest.CreationType = CreationType.FromService;
                break;
            case CurrentUserType.User:
                multiWayCreatableRequest.CreationType = CreationType.Manual;
                break;

        }
    }
    private void MatchPlayerId(IPlayerAuthraizedRequest playerAuthraizedRequest)
    {
        if (_currentUser.Role == UserRole.Player.Name)
            playerAuthraizedRequest.PlayerId = _currentUser.Id;
    }
    private void MatchCreatorId(ICreatorAuthraizedRequest creatorAuthraizedRequest)
    {
        if (_currentUser.Role != UserRole.Player.Name && creatorAuthraizedRequest.IncludeCreateror)
            creatorAuthraizedRequest.CreatorId = _currentUser.Id;
    }

}

