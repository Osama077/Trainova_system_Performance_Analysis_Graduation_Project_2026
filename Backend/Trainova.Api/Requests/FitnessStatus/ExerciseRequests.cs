using Trainova.Application.FitnessStatus.Exercises.Commands.CreateExercise;
using Trainova.Application.FitnessStatus.Exercises.Commands.UpdateExercise;
using Trainova.Domain.FitnessStatus.Enums;

namespace Trainova.Api.Requests.FitnessStatus
{
    public class CreateExerciseRequest
    {
        public string Name { get; set; }
        public ExerciseType Type { get; set; }
        public ExerciseCatagory Category { get; set; }
        public EquipmentRequired EquipmentRequired { get; set; }
        public MuscleGroup TargetMuscleGroups { get; set; }
        public ExerciseIntensity DefaultIntensity { get; set; }
        public int DefaultSets { get; set; }
        public string DefaultRepsOrDuration { get; set; }
        public int? DefaultRestBetweenSetsSec { get; set; }
        public string TypicalLoad { get; set; }
        public int? RecoveryTimeHours { get; set; }
        public string Description { get; set; }
        public string Contraindications { get; set; }

        public CreateExerciseCommand ToCommand()
        {
            return new CreateExerciseCommand(
                Name: Name,
                Type: Type,
                Category: Category,
                EquipmentRequired: EquipmentRequired,
                TargetMuscleGroups: TargetMuscleGroups,
                DefaultIntensity: DefaultIntensity,
                DefaultSets: DefaultSets,
                DefaultRepsOrDuration: DefaultRepsOrDuration,
                DefaultRestBetweenSetsSec: DefaultRestBetweenSetsSec,
                TypicalLoad: TypicalLoad,
                RecoveryTimeHours: RecoveryTimeHours,
                Description: Description,
                Contraindications: Contraindications
            );
        }
    }

    public class UpdateExerciseRequest
    {
        public string Name { get; set; }
        public ExerciseType Type { get; set; }
        public ExerciseCatagory Category { get; set; }
        public EquipmentRequired EquipmentRequired { get; set; }
        public MuscleGroup TargetMuscleGroups { get; set; }
        public ExerciseIntensity DefaultIntensity { get; set; }
        public int DefaultSets { get; set; }
        public string DefaultRepsOrDuration { get; set; }
        public int? DefaultRestBetweenSetsSec { get; set; }
        public string Description { get; set; }
        public string Contraindications { get; set; }

        public UpdateExerciseCommand ToCommand(Guid id)
        {
            return new UpdateExerciseCommand(
                Id: id,
                Name: Name,
                Type: Type,
                Category: Category,
                EquipmentRequired: EquipmentRequired,
                TargetMuscleGroups: TargetMuscleGroups,
                DefaultIntensity: DefaultIntensity,
                DefaultSets: DefaultSets,
                DefaultRepsOrDuration: DefaultRepsOrDuration,
                DefaultRestBetweenSetsSec: DefaultRestBetweenSetsSec,
                Description: Description,
                Contraindications: Contraindications
            );
        }
    }
}
