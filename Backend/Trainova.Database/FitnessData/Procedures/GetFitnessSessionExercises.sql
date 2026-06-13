
CREATE OR ALTER PROCEDURE FitnessData.sp_GetFitnessSessionExercises
    @SessionId UNIQUEIDENTIFIER = NULL,
    @ExerciseId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        *
    FROM FitnessData.vw_FitnessSessionExercises WITH (NOEXPAND)
    WHERE 
        (@SessionId IS NULL OR SessionId = @SessionId)
        AND 
        (@ExerciseId IS NULL OR ExerciseId = @ExerciseId);
END;
GO