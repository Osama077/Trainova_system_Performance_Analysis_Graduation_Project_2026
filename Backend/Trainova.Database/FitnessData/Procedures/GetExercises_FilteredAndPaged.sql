CREATE OR ALTER PROCEDURE [FitnessData].[sp_GetExercises_FilteredAndPaged]
    @ExerciseCatagory INT = NULL, -- تم إرجاع اسم البارامتر القديم لتجنب كسر كود التطبيق
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

    -- Validate and whitelist Sort Column based on exact column names from the image
    IF LOWER(@SortBy) NOT IN (
        'id', 'name', 'type', 'createdby', 'createdat', 'lastupdate', 
        'category', 'defaultsets', 'equipmentrequired', 'defaultintensity', 
        'targetmusclegroup', 'contraindications', 'defaultrepsorduration', 
        'defaultrestbetweensetssec', 'description', 'recoverytimehours', 'typicalload'
    )
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
            [Category],
            [DefaultSets],
            [EquipmentRequired],
            [DefaultIntensity],
            [TargetMuscleGroup],
            [Contraindications],
            [DefaultRepsOrDuration],
            [DefaultRestBetweenSetsSec],
            [Description],
            [RecoveryTimeHours],
            [TypicalLoad]
        FROM [dbo].[FitnessExercises]
        WHERE 
            (@pExerciseCatagory IS NULL OR [Category] = @pExerciseCatagory) -- ربط البارامتر بالعمود الصحيح
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