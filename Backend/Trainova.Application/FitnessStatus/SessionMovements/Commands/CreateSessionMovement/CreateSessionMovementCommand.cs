using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Application.FitnessStatus.SessionMovements.Commands.CreateSessionMovement
{
    [Authorize(Roles = "FitnessCoach,Player,Internal,Integrated")]
    public record CreateSessionMovementCommand(
        Guid UserAccessPolicyId,
        int SprintsCount,
        int DurationInMinutes,
        decimal WalkDistance,
        decimal RunDistance,
        decimal HighSpeedRunDistance,
        decimal AverageSpeed,
        decimal MaxSpeed,
        decimal PeakAcceleration) : IRequest<ResultOf<SessionMovement>>, IPhysicalMachineAuthraizedRequest
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

        CreationType IPhysicalMachineAuthraizedRequest.CreationType { get => CreationType; set => CreationType = value; }
    }
}
