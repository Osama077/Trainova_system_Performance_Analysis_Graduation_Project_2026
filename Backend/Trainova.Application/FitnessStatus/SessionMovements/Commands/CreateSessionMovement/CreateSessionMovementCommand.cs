using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Application.FitnessStatus.SessionMovements.Commands.CreateSessionMovement
{
    [Authorize(Roles = "FitnessCoach,Player,Internal,Integrated")]
    public record CreateSessionMovementCommand : IRequest<ResultOf<SessionMovement>>, IPhysicalMachineAuthraizedRequest
    {
        public Guid UserAccessPolicyId { get; init; }
        public int SprintsCount { get; init; }
        public decimal WalkDistance { get; init; }
        public decimal RunDistance { get; init; }
        public decimal HighSpeedRunDistance { get; init; }
        public decimal AverageSpeed { get; init; }
        public decimal MaxSpeed { get; init; }
        public decimal PeakAcceleration { get; init; }

        public CreationType CreationType { get; private set; }
        public CreateSessionMovementCommand(
        Guid userAccessPolicyId,
        int sprintsCount,
        decimal walkDistance,
        decimal runDistance,
        decimal highSpeedRunDistance,
        decimal averageSpeed,
        decimal maxSpeed,
        decimal peakAcceleration)
        {
            UserAccessPolicyId = userAccessPolicyId;
            SprintsCount = sprintsCount;
            WalkDistance = walkDistance;
            RunDistance = runDistance;
            HighSpeedRunDistance = highSpeedRunDistance;
            AverageSpeed = averageSpeed;
            MaxSpeed = maxSpeed;
            PeakAcceleration = peakAcceleration;
        }

        CreationType IPhysicalMachineAuthraizedRequest.CreationType { get => CreationType; set => CreationType = value; }
    }
}
