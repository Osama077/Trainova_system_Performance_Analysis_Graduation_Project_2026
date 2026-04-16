
CREATE OR ALTER PROCEDURE dbo.sp_GetPlayersFiltered_v1_1
    @PlayerId UNIQUEIDENTIFIER = NULL,
    @SearchTerm NVARCHAR(100) = NULL,
    @TeamId UNIQUEIDENTIFIER = NULL,
    @PerformanceLevel INT = NULL,
    @IsActive BIT = NULL,
    @MainPositionFilter INT = NULL,
    @OtherPositionFilter INT = NULL,
    @DateFrom DATETIME = NULL,
    @DateTo DATETIME = NULL,
    @MinMatches INT = NULL,
    @MedicalStatus NVARCHAR(50) = NULL,
    -- Pagination Parameters
    @PageNumber INT = 0,            -- الصفحة الحالية (تبدأ من 0)
    @PageSize INT = 12              -- عدد العناصر في الصفحة (الديفولت 12)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM View_PlayerDetails
    WHERE 
        (@PlayerId IS NULL OR Id = @PlayerId)
        AND (@SearchTerm IS NULL OR FullName LIKE '%' + @SearchTerm + '%' OR ShowName LIKE '%' + @SearchTerm + '%')
        AND (@TeamId IS NULL OR TeamId = @TeamId)
        AND (@PerformanceLevel IS NULL OR PerformanceLevel = @PerformanceLevel)
        AND (@IsActive IS NULL OR IsActive = @IsActive)
        AND (@MedicalStatus IS NULL OR MedicalStatus = @MedicalStatus)
        AND (@DateFrom IS NULL OR DateOfEnrolment >= @DateFrom)
        AND (@DateTo IS NULL OR DateOfEnrolment <= @DateTo)
        AND (@MinMatches IS NULL OR MatchesCount >= @MinMatches)
        AND (@MainPositionFilter IS NULL OR (CurrentMainPosition & @MainPositionFilter) = @MainPositionFilter)
        AND (@OtherPositionFilter IS NULL OR (OtherAvailablePositions & @OtherPositionFilter) = @OtherPositionFilter)
        
    ORDER BY CreatedAt DESC -- لازم ترتيب عشان الـ Offset يشتغل صح
    OFFSET (@PageNumber * @PageSize) ROWS 
    FETCH NEXT @PageSize ROWS ONLY;
END;
GO

sp_GetPlayersFiltered @SearchTerm = 'Osama'

GO


CREATE OR ALTER PROCEDURE playersData.sp_GetPlayersFiltered
    @PlayerId UNIQUEIDENTIFIER = NULL,
    @SearchTerm NVARCHAR(100) = NULL,
    @TeamId UNIQUEIDENTIFIER = NULL,
    @PerformanceLevel INT = NULL,
    @IsActive BIT = NULL,
    @MainPositionFilter INT = NULL,
    @OtherPositionFilter INT = NULL,
    @DateFrom DATETIME = NULL,
    @DateTo DATETIME = NULL,
    @MinMatches INT = NULL,
    @MedicalStatus NVARCHAR(50) = NULL,
    @PageNumber INT = 0,
    @PageSize INT = 12,
    -- Dynamic Sorting Parameters
    @SortColumn NVARCHAR(50) = 'CreatedAt',
    @SortDirection NVARCHAR(4) = 'DESC'
AS
BEGIN
    SET NOCOUNT ON;

    IF @SortDirection NOT IN ('ASC', 'DESC') SET @SortDirection = 'DESC';
    
    IF @SortColumn NOT IN ('CreatedAt', 'FullName', 'ShowName', 'PerformanceLevel', 'DateOfEnrolment', 'MatchesCount', 'InjuriesCount', 'MedicalStatus')
        SET @SortColumn = 'CreatedAt';

    DECLARE @Sql NVARCHAR(MAX);
    DECLARE @Where NVARCHAR(MAX) = N' WHERE 1=1';
    DECLARE @Params NVARCHAR(MAX) = N'
        @P_PlayerId UNIQUEIDENTIFIER, @P_SearchTerm NVARCHAR(100), 
        @P_TeamId UNIQUEIDENTIFIER, @P_PerformanceLevel INT, 
        @P_IsActive BIT, @P_MainPositionFilter INT, 
        @P_OtherPositionFilter INT, @P_DateFrom DATETIME, 
        @P_DateTo DATETIME, @P_MinMatches INT, 
        @P_MedicalStatus NVARCHAR(50), @P_PageNumber INT, @P_PageSize INT';

    IF @PlayerId IS NOT NULL SET @Where += N' AND Id = @P_PlayerId';
    IF @SearchTerm IS NOT NULL SET @Where += N' AND (FullName LIKE ''%'' + @P_SearchTerm + ''%'' OR ShowName LIKE ''%'' + @P_SearchTerm + ''%'')';
    IF @TeamId IS NOT NULL SET @Where += N' AND TeamId = @P_TeamId';
    IF @PerformanceLevel IS NOT NULL SET @Where += N' AND PerformanceLevel = @P_PerformanceLevel';
    IF @IsActive IS NOT NULL SET @Where += N' AND IsActive = @P_IsActive';
    IF @MedicalStatus IS NOT NULL SET @Where += N' AND MedicalStatus = @P_MedicalStatus';
    IF @DateFrom IS NOT NULL SET @Where += N' AND DateOfEnrolment >= @P_DateFrom';
    IF @DateTo IS NOT NULL SET @Where += N' AND DateOfEnrolment <= @P_DateTo';
    IF @MinMatches IS NOT NULL SET @Where += N' AND MatchesCount >= @P_MinMatches';
    IF @MainPositionFilter IS NOT NULL SET @Where += N' AND (CurrentMainPosition & @P_MainPositionFilter) = @P_MainPositionFilter';
    IF @OtherPositionFilter IS NOT NULL SET @Where += N' AND (OtherAvailablePositions & @P_OtherPositionFilter) = @P_OtherPositionFilter';

    SET @Sql = N'
        SELECT *, COUNT(*) OVER() AS TotalCount 
        FROM PlayersData.View_PlayerDetails' 
        + @Where + 
        N' ORDER BY ' + QUOTENAME(@SortColumn) + N' ' + @SortDirection + 
        N' OFFSET (@P_PageNumber * @P_PageSize) ROWS 
        FETCH NEXT @P_PageSize ROWS ONLY;';

    EXEC sp_executesql @Sql, @Params, 
        @P_PlayerId = @PlayerId, @P_SearchTerm = @SearchTerm, 
        @P_TeamId = @TeamId, @P_PerformanceLevel = @PerformanceLevel, 
        @P_IsActive = @IsActive, @P_MainPositionFilter = @MainPositionFilter, 
        @P_OtherPositionFilter = @OtherPositionFilter, @P_DateFrom = @DateFrom, 
        @P_DateTo = @DateTo, @P_MinMatches = @MinMatches, 
        @P_MedicalStatus = @MedicalStatus, @P_PageNumber = @PageNumber, 
        @P_PageSize = @PageSize;
END;
GO


PlayersData.sp_GetPlayersFiltered @MedicalStatus = 'Injured'