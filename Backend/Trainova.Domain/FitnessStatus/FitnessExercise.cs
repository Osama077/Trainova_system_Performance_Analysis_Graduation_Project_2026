using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.FitnessStatus.Enums;

namespace Trainova.Domain.FitnessStatus
{
    public class FitnessExercise : AuditableEntity<Guid>
    {
        public string Name { get; private set; }
        public EquipmentRequired EquipmentRequired { get; private set; }
        public MuscleGroup TargetMuscleGroup { get; private set; }
        public ExerciseIntensity DefaultIntensity { get; private set; }
        public ExerciseCatagory Category { get; private set; }
        public ExerciseType Type { get; private set; }

        public int DefaultSets { get; private set; }
        public string DefaultRepsOrDuration { get; private set; }
        public int? DefaultRestBetweenSetsSec { get; private set; }
        public string TypicalLoad { get; private set; }
        public int? RecoveryTimeHours { get; private set; }

        public string Description { get; private set; }
        public string Contraindications { get; private set; }

        public ICollection<FitnessSessionExercise> SessionExercises { get; private set; } = new List<FitnessSessionExercise>();

        public FitnessExercise(
            string name,
            ExerciseType type,
            ExerciseCatagory category,
            EquipmentRequired equipmentRequired,
            MuscleGroup targetMuscleGroup,
            ExerciseIntensity defaultIntensity,
            int defaultSets,
            string defaultRepsOrDuration,
            int? defaultRestBetweenSetsSec = null,
            string typicalLoad = null,
            int? recoveryTimeHours = null,
            string description = null,
            string contraindications = null,
            Guid? createdBy = null)
            : base(Guid.NewGuid(), createdBy)
        {
            Name = name;
            Type = type;
            Category = category;
            EquipmentRequired = equipmentRequired;
            TargetMuscleGroup = targetMuscleGroup;
            DefaultIntensity = defaultIntensity;
            DefaultSets = defaultSets;
            DefaultRepsOrDuration = defaultRepsOrDuration;
            DefaultRestBetweenSetsSec = defaultRestBetweenSetsSec;
            TypicalLoad = typicalLoad;
            RecoveryTimeHours = recoveryTimeHours;
            Description = description;
            Contraindications = contraindications;
        }

        private FitnessExercise() : base() { }

        public void Update(
            string? name = null,
            ExerciseType? type = null,
            ExerciseCatagory? category = null,
            EquipmentRequired? equipmentRequired = null,
            MuscleGroup? targetMuscleGroup = null,
            ExerciseIntensity? defaultIntensity = null,
            int? defaultSets = null,
            string? defaultRepsOrDuration = null,
            int? defaultRestBetweenSetsSec = null,
            string? description = null,
            string? contraindications = null)
        {
            MarkUpdatedNow();
            Name = name ?? Name;
            Type = type ?? Type;
            Category = category ?? Category;
            EquipmentRequired = equipmentRequired ?? EquipmentRequired;
            TargetMuscleGroup = targetMuscleGroup ?? TargetMuscleGroup;
            DefaultIntensity = defaultIntensity ?? DefaultIntensity;
            DefaultSets = defaultSets ?? DefaultSets;
            DefaultRepsOrDuration = defaultRepsOrDuration ?? DefaultRepsOrDuration;
            DefaultRestBetweenSetsSec = defaultRestBetweenSetsSec ?? DefaultRestBetweenSetsSec;
            Description = description ?? Description;
            Contraindications = contraindications ?? Contraindications;
        }
    }

}
