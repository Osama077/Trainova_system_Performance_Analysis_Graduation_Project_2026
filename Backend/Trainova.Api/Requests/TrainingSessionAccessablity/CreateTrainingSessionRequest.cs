using Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.CreateTrainingSession;
using Trainova.Domain.Common.Enums;

namespace Trainova.Api.Requests.TrainingSessionAccessablity
{
    public class CreateTrainingSessionRequest
    {
        public Guid? PolicyId { get; set; } = null;
        public string SessionName { get; set; }
        public PlanState PlanState { get; set; } = PlanState.Active;
        public string? Place { get; set; } = null;
        public DateTime? WillHappenAt { get; set; } = null;
        public Guid? PlanId { get; set; } = null;

        public List<Guid> UserIds { get; set; } = new List<Guid>();


        public CreateTrainingSessionCommand ToCommand()
        {
            return new CreateTrainingSessionCommand(
                SessionName,
                PolicyId,
                PlanState,
                Place,
                WillHappenAt,
                PlanId,
                UserIds
                );
        }
    }
}
