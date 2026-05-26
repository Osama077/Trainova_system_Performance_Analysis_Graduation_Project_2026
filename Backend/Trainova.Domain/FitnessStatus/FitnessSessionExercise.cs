using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Domain.FitnessStatus
{
    public class FitnessSessionExercise : AuditableEntity<Guid>
    {
        public Guid SessionId { get; private set; }
        public TrainingSession Session { get; private set; }

        public Guid ExerciseId { get; private set; }
        public FitnessExercise Exercise { get; private set; }

        public int? Sets { get; private set; }
        public int? Reps { get; private set; }
        public int? Rounds { get; private set; }

        public int? ActiveTimeSec { get; private set; }
        public int? RestTimeSec { get; private set; }

        public string LoadDetails { get; private set; }

        public ExerciseIntensity Intensity { get; private set; }

        private FitnessSessionExercise() : base()
        {
        }
        public void Update(
            ExerciseIntensity? intensity = null,
            int? sets = null,
            int? reps = null,
            int? rounds = null,
            int? activeTimeSec = null,
            int? restTimeSec = null,
            string loadDetails = null)
        {
            MarkUpdatedNow();
            Intensity = intensity ?? Intensity;
            Sets = sets ?? Sets;
            Reps = reps ?? Reps;
            Rounds = rounds ?? Rounds;
            ActiveTimeSec = activeTimeSec ?? ActiveTimeSec;
            RestTimeSec = restTimeSec ?? RestTimeSec;
            LoadDetails = loadDetails ?? LoadDetails;
        }
        public FitnessSessionExercise(
            Guid sessionId,
            FitnessExercise exercise,
            ExerciseIntensity intensity,
            Guid? createdBy = null,
            int? sets = null,
            int? reps = null,
            int? rounds = null,
            int? activeTimeSec = null,
            int? restTimeSec = null,
            string loadDetails = null)
            : base(Guid.NewGuid(), createdBy)
        {
            if (exercise == null)
                throw new DomainException(
                    "Exercise cannot be null.",
                    "DomainError_ExerciseRequired");

            switch (exercise.Type)
            {
                case ExerciseType.Repetitions:
                case ExerciseType.Bodyweight:
                    if (!sets.HasValue || !reps.HasValue)
                        throw new DomainException(
                            $"Exercises of type '{exercise.Type}' must have both Sets and Reps defined.",
                            "DomainError_InvalidRepetitionVolume");

                    if (activeTimeSec.HasValue)
                        throw new DomainException(
                            $"Exercises of type '{exercise.Type}' cannot have Active Time duration.",
                            "DomainError_DurationNotAllowed");
                    break;

                case ExerciseType.Duration:
                    if (!rounds.HasValue || !activeTimeSec.HasValue)
                        throw new DomainException(
                            "Duration-based exercises must have Rounds and ActiveTimeSec defined.",
                            "DomainError_InvalidDurationVolume");

                    if (reps.HasValue)
                        throw new DomainException(
                            "Duration-based exercises cannot have specific Reps count.",
                            "DomainError_RepsNotAllowed");
                    break;

                case ExerciseType.Explosive:
                    if (!sets.HasValue && !rounds.HasValue)
                        throw new DomainException(
                            "Explosive exercises must have either Sets or Rounds defined.",
                            "DomainError_MissingExplosiveVolume");
                    break;
            }

            SessionId = sessionId;
            ExerciseId = exercise.Id;
            Exercise = exercise;
            Intensity = intensity;

            Sets = sets;
            Reps = reps;
            Rounds = rounds;
            ActiveTimeSec = activeTimeSec;
            RestTimeSec = restTimeSec;
            LoadDetails = loadDetails;
        }
    }
}
