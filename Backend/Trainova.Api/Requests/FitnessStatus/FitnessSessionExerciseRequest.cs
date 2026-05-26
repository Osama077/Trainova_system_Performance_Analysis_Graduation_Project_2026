using System;
using Trainova.Application.FitnessStatus.FitnessSessionExercises.Commands.CreateFitnessSessionExercise;
using Trainova.Application.FitnessStatus.FitnessSessionExercises.Commands.UpdateFitnessSessionExercise;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Api.Requests.FitnessStatus
{
    public class CreateFitnessSessionExerciseRequest
    {
        public Guid SessionId { get; set; }
        public Guid ExerciseId { get; set; }
        public ExerciseIntensity Intensity { get; set; }
        public int? Sets { get; set; }
        public int? Reps { get; set; }
        public int? Rounds { get; set; }
        public int? ActiveTimeSec { get; set; }
        public int? RestTimeSec { get; set; }
        public string? LoadDetails { get; set; }

        public CreateFitnessSessionExerciseCommand ToCommand()
        {
            return new CreateFitnessSessionExerciseCommand(
                SessionId: SessionId,
                ExerciseId: ExerciseId,
                Intensity: Intensity,
                Sets: Sets,
                Reps: Reps,
                Rounds: Rounds,
                ActiveTimeSec: ActiveTimeSec,
                RestTimeSec: RestTimeSec,
                LoadDetails: LoadDetails
            );
        }
    }

    public class UpdateFitnessSessionExerciseRequest
    {
        public ExerciseIntensity? Intensity { get; set; }
        public int? Sets { get; set; }
        public int? Reps { get; set; }
        public int? Rounds { get; set; }
        public int? ActiveTimeSec { get; set; }
        public int? RestTimeSec { get; set; }
        public string? LoadDetails { get; set; }

        public UpdateFitnessSessionExerciseCommand ToCommand(Guid id)
        {
            return new UpdateFitnessSessionExerciseCommand(
                Id: id,
                Intensity: Intensity,
                Sets: Sets,
                Reps: Reps,
                Rounds: Rounds,
                ActiveTimeSec: ActiveTimeSec,
                RestTimeSec: RestTimeSec,
                LoadDetails: LoadDetails
            );
        }
    }
}
