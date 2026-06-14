CREATE SCHEMA FitnessData;
GO

CREATE OR ALTER VIEW FitnessData.vw_FitnessSessionExercises 
WITH SCHEMABINDING AS
SELECT 
    fse.Id,
    fse.SessionId,
    fse.ExerciseId,
    fse.Sets,
    fse.RepsOrDuration,
    fse.RestTimeSec,
    fse.LoadDetails,
    fse.Intensity,
    fse.Rounds,
    fse.ActiveTimeSec,
    fse.CreatedAt,
    fse.LastUpdate,
    e.Name,
    e.EquipmentRequired,
    e.TargetMuscleGroup,
    e.DefaultIntensity,
    e.Type,
    s.TrainingSessionName,
    s.PlanId,
    s.AccessPolicyId,
    s.SessionType,
    s.SessionState,
    s.Place,
    s.HappenedAt
FROM dbo.FitnessSessionExercises fse
INNER JOIN dbo.FitnessExercises e ON fse.ExerciseId = e.Id
INNER JOIN dbo.TrainingSessions s ON fse.SessionId = s.Id;
GO

CREATE UNIQUE CLUSTERED INDEX UIX_vw_FitnessSessionExercises_Session_Exercise
ON FitnessData.vw_FitnessSessionExercises (SessionId,ExerciseId);
GO


