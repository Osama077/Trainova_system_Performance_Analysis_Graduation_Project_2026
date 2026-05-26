using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trainova.Application.Common.Authorization;
using Trainova.Application.Common.Interfaces.Repositories.FitnessStatus;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Application.FitnessStatus.SessionMovements.Commands.CreateSessionMovement
{
    [Authorize(Roles = "Coach")]
    public record CreateSessionMovementCommand(
        Guid UserAccessPolicyId,
        int SprintsCount,
        decimal? WalkDistance = null,
        decimal? RunDistance = null,
        decimal? HighSpeedRunDistance = null,
        decimal? AverageSpeed = null,
        decimal? MaxSpeed = null,
        decimal? PeakAcceleration = null,
        decimal? PlayerLoad = null) : IRequest<ResultOf<SessionMovement>>;

    public class CreateSessionMovementCommandHandler : IRequestHandler<CreateSessionMovementCommand, ResultOf<SessionMovement>>
    {
        private readonly ISessionMovementRepository _sessionMovementRepository;
        private readonly IUserAccessPolicyRepository _userAccessPolicyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSessionMovementCommandHandler(
            ISessionMovementRepository sessionMovementRepository,
            IUserAccessPolicyRepository userAccessPolicyRepository,
            IUnitOfWork unitOfWork)
        {
            _sessionMovementRepository = sessionMovementRepository;
            _userAccessPolicyRepository = userAccessPolicyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultOf<SessionMovement>> Handle(CreateSessionMovementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userAccessPolicy = await _userAccessPolicyRepository.GetByIdAsync(request.UserAccessPolicyId);
                if (userAccessPolicy == null)
                    return Error.NotFound(description: "User access policy not found.");

                var existing = await _sessionMovementRepository.GetByUserAccessPolicyIdAsync(request.UserAccessPolicyId);
                if (existing != null)
                    return Error.Conflict(description: "Session movement stats already exist for this user policy. Use Update instead.");

                Distance? distance = null;
                if (request.WalkDistance.HasValue || request.RunDistance.HasValue || request.HighSpeedRunDistance.HasValue)
                {
                    distance = new Distance(
                        request.WalkDistance ?? 0,
                        request.RunDistance ?? 0,
                        request.HighSpeedRunDistance ?? 0
                    );
                }

                Speed? speed = null;
                if (request.AverageSpeed.HasValue || request.MaxSpeed.HasValue || request.PeakAcceleration.HasValue)
                {
                    speed = new Speed(
                        request.AverageSpeed ?? 0,
                        request.MaxSpeed ?? request.AverageSpeed ?? 0,
                        request.PeakAcceleration ?? 0
                    );
                }

                var movement = new SessionMovement(
                    request.UserAccessPolicyId,
                    request.SprintsCount,
                    distance,
                    speed
                );

                if (request.PlayerLoad.HasValue)
                {
                    movement.Update(null, null, null, request.PlayerLoad.Value);
                }

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
