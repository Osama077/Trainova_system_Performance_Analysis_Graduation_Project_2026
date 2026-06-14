CREATE OR ALTER PROCEDURE FitnessData.sp_GetPlayerLoadVsCapacityTimeline
    @PlayerId UNIQUEIDENTIFIER,
    @FromDate DATETIME = NULL,
    @ToDate DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Get Capacity Tests Data
    SELECT 
        TestCreatedAt AS RecordDate,
        1 AS SourceType, -- CapacityTest
        ISNULL(OverriddenCapacity, CalculatedCapacity) AS Value
    FROM FitnessData.vw_PlayerTestsSummary
    WHERE UserId = @PlayerId
      AND (@FromDate IS NULL OR TestCreatedAt >= @FromDate)
      AND (@ToDate IS NULL OR TestCreatedAt <= @ToDate)

    UNION ALL

    -- 2. Get Session Movements Data
    SELECT 
        SessionCreatedAt AS RecordDate,
        2 AS SourceType, -- SessionMovement
        ISNULL(OverriddenLoad, PlayerCalculatedLoad) AS Value
    FROM FitnessData.vw_PlayerSessionsSummary
    WHERE UserId = @PlayerId
      AND (@FromDate IS NULL OR SessionCreatedAt >= @FromDate)
      AND (@ToDate IS NULL OR SessionCreatedAt <= @ToDate)

    ORDER BY RecordDate ASC; -- ASC is critical for drawing continuous timeline charts
END;
GO