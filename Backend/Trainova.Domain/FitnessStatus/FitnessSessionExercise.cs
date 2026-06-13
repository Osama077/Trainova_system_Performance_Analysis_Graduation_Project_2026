using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.FitnessStatus.Enums;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Domain.FitnessStatus
{
    public class FitnessSessionExercise : AuditableEntity<Guid>
    {
        public Guid SessionId { get; private set; }
        public TrainingSession Session { get; private set; }

        public Guid ExerciseId { get; private set; }
        public FitnessExercise Exercise { get; private set; }

        public int Sets { get; private set; }
        public string RepsOrDuration { get; private set; }
        public int? RestTimeSec { get; private set; }
        public string LoadDetails { get; private set; }
        public ExerciseIntensity Intensity { get; private set; }

        public int? Rounds { get; private set; }
        public int? ActiveTimeSec { get; private set; }

        private FitnessSessionExercise() : base() { }

        public FitnessSessionExercise(
            Guid sessionId,
            FitnessExercise exercise,
            ExerciseIntensity? intensity = null,
            int? sets = null,
            string repsOrDuration = null,
            int? restTimeSec = null,
            string loadDetails = null,
            int? rounds = null,
            int? activeTimeSec = null,
            Guid? createdBy = null)
            : base(Guid.NewGuid(), createdBy)
        {
            if (exercise == null)
                throw new DomainException("Exercise cannot be null.", "DomainError_ExerciseRequired");

            SessionId = sessionId;
            ExerciseId = exercise.Id;
            Exercise = exercise;

            Intensity = intensity ?? exercise.DefaultIntensity;
            Sets = sets ?? exercise.DefaultSets;
            RepsOrDuration = repsOrDuration ?? exercise.DefaultRepsOrDuration;
            RestTimeSec = restTimeSec ?? exercise.DefaultRestBetweenSetsSec;
            LoadDetails = loadDetails ?? exercise.TypicalLoad;

            Rounds = rounds;
            ActiveTimeSec = activeTimeSec;

            ValidateExerciseTypeRules(exercise);
        }

        private void ValidateExerciseTypeRules(FitnessExercise exercise)
        {
            switch (exercise.Type)
            {
                case ExerciseType.Repetitions:
                case ExerciseType.Bodyweight:
                    if (ActiveTimeSec.HasValue)
                        throw new DomainException($"Exercises of type '{exercise.Type}' cannot have Active Time duration.", "DomainError_DurationNotAllowed");
                    break;

                case ExerciseType.Duration:
                    if (!Rounds.HasValue && string.IsNullOrEmpty(RepsOrDuration))
                        throw new DomainException("Duration-based exercises must have Rounds or Duration defined.", "DomainError_InvalidDurationVolume");
                    break;
            }
        }

        public void Update(
            ExerciseIntensity? intensity = null,
            int? sets = null,
            string repsOrDuration = null,
            int? restTimeSec = null,
            string loadDetails = null,
            int? rounds = null,
            int? activeTimeSec = null)
        {
            MarkUpdatedNow();
            Intensity = intensity ?? Intensity;
            Sets = sets ?? Sets;
            RepsOrDuration = repsOrDuration ?? RepsOrDuration;
            RestTimeSec = restTimeSec ?? RestTimeSec;
            LoadDetails = loadDetails ?? LoadDetails;
            Rounds = rounds ?? Rounds;
            ActiveTimeSec = activeTimeSec ?? ActiveTimeSec;
        }
    }
}
