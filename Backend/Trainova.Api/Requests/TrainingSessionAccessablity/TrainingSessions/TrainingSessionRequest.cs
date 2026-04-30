using Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.CreateTrainingSession;
using Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.UpdateTrainingSession;
using Trainova.Domain.Common.Enums;

namespace Trainova.Api.Requests.TrainingSessionAccessablity.TrainingSessions
{
    public class CreateTrainingSessionRequest
    {
        public string SessionName { get; set; }
        public Guid? PolicyId { get; set; }
        public string? Place { get; set; }
        public DateTime? WillHappenAt { get; set; }
        public Guid? PlanId { get; set; }
        public List<Guid> UserIds { get; set; } = new();

        public CreateTrainingSessionCommand ToCommand()
        {
            return new CreateTrainingSessionCommand(
                SessionName,
                PolicyId,
                PlanState.Active,
                Place,
                WillHappenAt,
                PlanId,
                UserIds);
        }
    }

    public class UpdateTrainingSessionRequest
    {
        public string? SessionName { get; set; }
        public string? Place { get; set; }
        public DateTime? WillHappenAt { get; set; }
        public PlanState? State { get; set; }

        public UpdateTrainingSessionCommand ToCommand(Guid id)
        {
            return new UpdateTrainingSessionCommand(
                id,
                SessionName,
                Place,
                WillHappenAt,
                State);
        }
    }
}
