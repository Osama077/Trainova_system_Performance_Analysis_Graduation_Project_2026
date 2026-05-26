using Trainova.Domain.Common.BaseEntity;

namespace Trainova.Domain.FitnessStatus
{
    public class FitnessExercise : AuditableEntity<Guid>
    {
        public string Name { get; private set; }

        public ExerciseType Type { get; private set; }

        public ICollection<FitnessSessionExercise> SessionExercises { get; private set; } = new List<FitnessSessionExercise>();

        public FitnessExercise(string name, ExerciseType type, Guid? createdBy = null)
            : base(Guid.NewGuid(), createdBy)
        {
            Name = name;
            Type = type;
        }

        private FitnessExercise() : base()
        {
        }


        public void Update(string? name, ExerciseType? type)
        {
            MarkUpdatedNow();
            Name = name ?? Name;
            Type = type ?? Type;
        }
    }
}
