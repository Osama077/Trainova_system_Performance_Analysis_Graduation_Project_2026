using Trainova.Application.FitnessStatus.SessionMovements.Commands.CreateSessionMovement;
using Trainova.Application.FitnessStatus.SessionMovements.Commands.UpdateSessionMovement;

namespace Trainova.Api.Requests.FitnessStatus
{
    public class CreateSessionMovementRequest
    {
        public Guid UserAccessPolicyId { get; set; }
        public int SprintsCount { get; set; }
        public int DurationInMinutes { get; set; }
        public decimal WalkDistance { get; set; }
        public decimal RunDistance { get; set; }
        public decimal HighSpeedRunDistance { get; set; }
        public decimal AverageSpeed { get; set; }
        public decimal MaxSpeed { get; set; }
        public decimal PeakAcceleration { get; set; }

        public CreateSessionMovementCommand ToCommand()
        {
            return new CreateSessionMovementCommand(
                userAccessPolicyId: UserAccessPolicyId,
                sprintsCount: SprintsCount,
                walkDistance: WalkDistance,
                runDistance: RunDistance,
                highSpeedRunDistance: HighSpeedRunDistance,
                averageSpeed: AverageSpeed,
                maxSpeed: MaxSpeed,
                peakAcceleration: PeakAcceleration
            );
        }
    }

    public class UpdateSessionMovementRequest
    {
        public int? SprintsCount { get; set; }
        public int? DurationInMinutes { get; set; }
        public decimal? WalkDistance { get; set; }
        public decimal? RunDistance { get; set; }
        public decimal? HighSpeedRunDistance { get; set; }
        public decimal? AverageSpeed { get; set; }
        public decimal? MaxSpeed { get; set; }
        public decimal? PeakAcceleration { get; set; }

        public UpdateSessionMovementCommand ToCommand(Guid id)
        {
            return new UpdateSessionMovementCommand(
                Id: id,
                SprintsCount: SprintsCount,
                DurationInMinutes: DurationInMinutes,
                WalkDistance: WalkDistance,
                RunDistance: RunDistance,
                HighSpeedRunDistance: HighSpeedRunDistance,
                AverageSpeed: AverageSpeed,
                MaxSpeed: MaxSpeed,
                PeakAcceleration: PeakAcceleration
            );
        }
    }
}
