CREATE OR ALTER PROCEDURE [FitnessData].[sp_GetExercises_FilteredAndPaged]
    @ExerciseCatagory INT = NULL,
    @Search NVARCHAR(255) = NULL,
    @Page INT = 0,
    @PageSize INT = 12,
    @SortBy NVARCHAR(50) = 'CreatedAt',
    @SortDir NVARCHAR(10) = 'ASC'
AS
BEGIN
    SET NOCOUNT ON;

    -- Validate and whitelist Sort Direction to prevent SQL Injection
    IF LOWER(@SortDir) NOT IN ('asc', 'desc')
    BEGIN
        SET @SortDir = 'ASC';
    END

    -- Validate and whitelist Sort Column based on your exact column names
    IF LOWER(@SortBy) NOT IN ('id', 'name', 'type', 'createdby', 'createdat', 'lastupdate', 'defaultexerciseintensity', 'defaultrepetitions', 'defaultsets', 'equipmentrequired', 'exercisecatagory', 'targetmusclegroup')
    BEGIN
        SET @SortBy = 'CreatedAt';
    END

    -- Safe execution using dynamic SQL for proper sorting configuration
    DECLARE @SQL NVARCHAR(MAX);
    DECLARE @ParmDefinition NVARCHAR(500);

    SET @SQL = N'
        SELECT
            [Id],
            [Name],
            [Type],
            [CreatedBy],
            [CreatedAt],
            [LastUpdate],
            [DefaultExerciseIntensity],
            [DefaultRepetitions],
            [DefaultSets],
            [EquipmentRequired],
            [ExerciseCatagory],
            [TargetMuscleGroup]
        FROM [dbo].[FitnessExercises]
        WHERE 
            (@pExerciseCatagory IS NULL OR [ExerciseCatagory] = @pExerciseCatagory)
            AND (@pSearch IS NULL OR [Name] LIKE ''%'' + @pSearch + ''%'')
        ORDER BY ' + QUOTENAME(@SortBy) + ' ' + @SortDir + '
        OFFSET @pPage * @pPageSize ROWS
        FETCH NEXT @pPageSize ROWS ONLY;';

    SET @ParmDefinition = N'
        @pExerciseCatagory INT,
        @pSearch NVARCHAR(255),
        @pPage INT,
        @pPageSize INT';

    EXEC sp_executesql @SQL, @ParmDefinition,
        @pExerciseCatagory = @ExerciseCatagory,
        @pSearch = @Search,
        @pPage = @Page,
        @pPageSize = @PageSize;
END