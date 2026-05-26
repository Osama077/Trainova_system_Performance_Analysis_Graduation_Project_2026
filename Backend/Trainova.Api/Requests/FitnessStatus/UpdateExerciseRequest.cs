using Trainova.Application.FitnessStatus.Exercises.Commands.UpdateExercise;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Api.Requests.FitnessStatus
{
    public class UpdateExerciseRequest
    {

        public string Name { get; set; } = null;
        public ExerciseType? Type { get; set; } = null;

        public UpdateExerciseCommand ToCommand(Guid id)
        {
            return new UpdateExerciseCommand(
                Id: id,
                Name: Name,
                Type: Type
            );
        }
    }
}
