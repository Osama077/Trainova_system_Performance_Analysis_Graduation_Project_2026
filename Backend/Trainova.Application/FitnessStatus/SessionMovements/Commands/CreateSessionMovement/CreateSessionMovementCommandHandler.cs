using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.FitnessStatus;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.FitnessStatus;
using Trainova.Domain.UserAuth;

namespace Trainova.Application.FitnessStatus.SessionMovements.Commands.CreateSessionMovement
{
    public class CreateSessionMovementCommandHandler(
        ISessionMovementRepository _sessionMovementRepository,
        IUserAccessPolicyRepository _userAccessPolicyRepository,
        IPhysicalCapacityTestRepository _physicalCapacityTestRepository,
        IUnitOfWork _unitOfWork,
        CurrentUser _currentUser)
        : IRequestHandler<CreateSessionMovementCommand, ResultOf<SessionMovement>>
    {
        public async Task<ResultOf<SessionMovement>> Handle(CreateSessionMovementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userAccessPolicy = await _userAccessPolicyRepository.GetByIdAsync(request.UserAccessPolicyId);
                if (userAccessPolicy == null)
                    return Error.NotFound(description: "User access policy not found.");

                if (_currentUser.Role == StaticRoleNamesData.PlayerName && _currentUser.Id != userAccessPolicy.UserId)
                    return Error.Unauthorized("DifferentPlayerAddingData.Unauthorized", "Can't add data to another player");




                var existing = await _sessionMovementRepository.GetByUserAccessPolicyIdAsync(request.UserAccessPolicyId);
                if (existing != null)
                    return Error.Conflict(description: "Session movement stats already exist for this user policy. Use Update instead.");

                var distance = new Distance(
                        request.WalkDistance,
                        request.RunDistance,
                        request.HighSpeedRunDistance
                    );


                var speed = new Speed(
                        request.AverageSpeed,
                        request.MaxSpeed,
                        request.PeakAcceleration
                    );

                var lasttest = await _physicalCapacityTestRepository.GetLatestByPlayerIdAsync(userAccessPolicy.UserId);
                var lastSessionMovement = await _sessionMovementRepository.GetLastByUserAccessPolicyIdAsync(request.UserAccessPolicyId);

                var movement = new SessionMovement(
                    request.UserAccessPolicyId,
                    request.SprintsCount,
                    request.DurationInMinutes,
                    distance,
                    speed,
                    lasttest,
                    lastSessionMovement
                );


                await _unitOfWork.StartTransactionAsync();
                await _sessionMovementRepository.AddAsync(movement);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return movement.AsCreated();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "CreateSessionMovementCommandHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }
}
