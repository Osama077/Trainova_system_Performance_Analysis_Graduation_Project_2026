using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Application.Common.Interfaces.Repositories.FitnessStatus;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Application.FitnessStatus.SessionMovements.Commands.UpdateSessionMovement
{
    [Authorize(Roles = "Coach")]
    public record UpdateSessionMovementCommand(
        Guid Id,
        int? SprintsCount = null,
        int? DurationInMinutes = null,
        decimal? WalkDistance = null,
        decimal? RunDistance = null,
        decimal? HighSpeedRunDistance = null,
        decimal? AverageSpeed = null,
        decimal? MaxSpeed = null,
        decimal? PeakAcceleration = null,
        decimal? PlayerLoad = null) : IRequest<ResultOf<SessionMovement>>;

    public class UpdateSessionMovementCommandHandler : IRequestHandler<UpdateSessionMovementCommand, ResultOf<SessionMovement>>
    {
        private readonly ISessionMovementRepository _sessionMovementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSessionMovementCommandHandler(
            ISessionMovementRepository sessionMovementRepository,
            IUnitOfWork unitOfWork)
        {
            _sessionMovementRepository = sessionMovementRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultOf<SessionMovement>> Handle(UpdateSessionMovementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var movement = await _sessionMovementRepository.GetByIdAsync(request.Id);
                if (movement == null)
                    return Error.NotFound(description: "Session movement stats not found.");

                Distance? distance = null;
                if (request.WalkDistance.HasValue || request.RunDistance.HasValue || request.HighSpeedRunDistance.HasValue)
                {
                    var currentDistance = movement.Distance;
                    distance = new Distance(
                        request.WalkDistance ?? currentDistance?.WalkDistance ?? 0,
                        request.RunDistance ?? currentDistance?.RunDistance ?? 0,
                        request.HighSpeedRunDistance ?? currentDistance?.HighSpeedRunDistance ?? 0
                    );
                }

                Speed? speed = null;
                if (request.AverageSpeed.HasValue || request.MaxSpeed.HasValue || request.PeakAcceleration.HasValue)
                {
                    var currentSpeed = movement.Speed;
                    var avg = request.AverageSpeed ?? currentSpeed?.AverageSpeed ?? 0;
                    var max = request.MaxSpeed ?? currentSpeed?.MaxSpeed ?? avg;
                    var acc = request.PeakAcceleration ?? currentSpeed?.PeakAcceleration ?? 0;
                    speed = new Speed(avg, max, acc);
                }

                movement.Update(
                    request.SprintsCount,
                    request.DurationInMinutes,
                    distance,
                    speed
                );

                await _unitOfWork.StartTransactionAsync();
                await _sessionMovementRepository.UpdateAsync(movement);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return movement.AsDone();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "UpdateSessionMovementCommandHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }
}
