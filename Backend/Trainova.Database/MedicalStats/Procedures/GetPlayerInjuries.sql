CREATE OR ALTER PROCEDURE dbo.GetPlayerInjuries
    @PlayerInjuryId UNIQUEIDENTIFIER = NULL,
    @PlayerId UNIQUEIDENTIFIER = NULL,
    @InjuryId UNIQUEIDENTIFIER = NULL,
    @Status NVARCHAR(50) = NULL,
    @Cause NVARCHAR(200) = NULL,
    @IsNew BIT = NULL,
    @HappendBefore DATETIME2 = NULL,
    @HappendAfter DATETIME2 = NULL,
    @ExpectedReturnBefore DATETIME2 = NULL,
    @ExpectedReturnAfter DATETIME2 = NULL,
    @ReturnedBefore DATETIME2 = NULL,
    @ReturnedAfter DATETIME2 = NULL,
    -- Pagination
    @PageNumber INT = 1,            -- Changed to 1-based to match standard logic
    @PageSize INT = 20,
    -- Dynamic Sorting
    @SortColumn NVARCHAR(100) = 'PlayerInjuryCreatedAt',
    @SortDirection NVARCHAR(4) = 'DESC'
    AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Validate Sort Direction
    IF @SortDirection NOT IN ('ASC', 'DESC') SET @SortDirection = 'DESC';
    
    -- 2. Validate Sort Column (Whitelist to prevent SQL Injection)
    IF @SortColumn NOT IN (
        'PlayerInjuryCreatedAt', 'PlayerNumber', 'PlayerId', 
        'InjuryName', 'Status', 'HappendAt', 
        'ExpectedReturnDate', 'ReturnedAt'
    )
    SET @SortColumn = 'PlayerInjuryCreatedAt';

    -- 3. Prepare Dynamic SQL Components
    DECLARE @Sql NVARCHAR(MAX);
    DECLARE @Where NVARCHAR(MAX) = N' WHERE 1=1';
    
    -- Define Parameter Types for sp_executesql
    DECLARE @Params NVARCHAR(MAX) = N'
        @P_PlayerInjuryId UNIQUEIDENTIFIER, @P_PlayerId UNIQUEIDENTIFIER, 
        @P_InjuryId UNIQUEIDENTIFIER, @P_Status NVARCHAR(50), 
        @P_Cause NVARCHAR(200), @P_IsNew BIT, 
        @P_HappendBefore DATETIME2, @P_HappendAfter DATETIME2, 
        @P_ExpectedReturnBefore DATETIME2, @P_ExpectedReturnAfter DATETIME2, 
        @P_ReturnedBefore DATETIME2, @P_ReturnedAfter DATETIME2, 
        @P_Offset INT, @P_PageSize INT';

    -- 4. Build Dynamic WHERE Clause
    IF @PlayerInjuryId IS NOT NULL SET @Where += N' AND PlayerInjuryId = @P_PlayerInjuryId';
    IF @PlayerId IS NOT NULL       SET @Where += N' AND PlayerId = @P_PlayerId';
    IF @InjuryId IS NOT NULL       SET @Where += N' AND InjuryId = @P_InjuryId';
    IF @Status IS NOT NULL         SET @Where += N' AND Status = @P_Status';
    IF @IsNew IS NOT NULL          SET @Where += N' AND IsNew = @P_IsNew';
    
    IF @Cause IS NOT NULL          
        SET @Where += N' AND Cause LIKE ''%'' + @P_Cause + ''%''';

    IF @HappendBefore IS NOT NULL  SET @Where += N' AND HappendAt <= @P_HappendBefore';
    IF @HappendAfter IS NOT NULL   SET @Where += N' AND HappendAt >= @P_HappendAfter';
    
    IF @ExpectedReturnBefore IS NOT NULL SET @Where += N' AND ExpectedReturnDate <= @P_ExpectedReturnBefore';
    IF @ExpectedReturnAfter IS NOT NULL  SET @Where += N' AND ExpectedReturnDate >= @P_ExpectedReturnAfter';
    
    IF @ReturnedBefore IS NOT NULL SET @Where += N' AND ReturnedAt <= @P_ReturnedBefore';
    IF @ReturnedAfter IS NOT NULL  SET @Where += N' AND ReturnedAt >= @P_ReturnedAfter';

    -- 5. Calculate Offset (Assuming 1-based PageNumber)
    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;
    IF @Offset < 0 SET @Offset = 0;

    -- 6. Combine and Execute
    SET @Sql = N'
        SELECT * 
        FROM dbo.View_PlayerInjury_Player_Injury' 
        + @Where + 
        N' ORDER BY ' + QUOTENAME(@SortColumn) + N' ' + @SortDirection + 
        N' OFFSET @P_Offset ROWS 
        FETCH NEXT @P_PageSize ROWS ONLY;';

EXEC sp_executesql @Sql, @Params, 
        @P_PlayerInjuryId = @PlayerInjuryId, 
        @P_PlayerId = @PlayerId, 
        @P_InjuryId = @InjuryId, 
        @P_Status = @Status, 
        @P_Cause = @Cause, 
        @P_IsNew = @IsNew, 
        @P_HappendBefore = @HappendBefore, 
        @P_HappendAfter = @HappendAfter, 
        @P_ExpectedReturnBefore = @ExpectedReturnBefore, 
        @P_ExpectedReturnAfter = @ExpectedReturnAfter, 
        @P_ReturnedBefore = @ReturnedBefore, 
        @P_ReturnedAfter = @ReturnedAfter, 
        @P_Offset = @Offset, 
        @P_PageSize = @PageSize;
END;
GO