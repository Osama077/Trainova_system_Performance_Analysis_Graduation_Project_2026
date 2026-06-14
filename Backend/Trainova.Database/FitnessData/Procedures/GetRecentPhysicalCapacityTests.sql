CREATE OR ALTER PROCEDURE FitnessData.sp_GetRecentPhysicalCapacityTests
    @PlayerId UNIQUEIDENTIFIER = NULL,
    @SearchName NVARCHAR(200) = NULL,
    @FromDate DATETIME = NULL,
    @ToDate DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        CapacityTestId AS TestId,
        UserId AS PlayerId,
        FullName AS PlayerName,
        
        -- Aerobic Capacity
        MaximumOxygenConsumption,
        YoYoIntermittentRecoveryLevel1Distance,
        YoYoIntermittentRecoveryLevel2Distance,
        
        -- Sprint
        Time10Meters,
        Time30Meters,
        
        -- Explosive Power
        CountermovementJumpHeight,
        ReactiveStrengthIndex,
        
        -- Meta
        CreationType,
        OverriddenCapacity,
        CalculatedCapacity,
        ProgressFromLastTest,
        TestCreatedAt AS CreatedAt
    FROM FitnessData.vw_PlayerTestsSummary
    WHERE 
        (@PlayerId IS NULL OR UserId = @PlayerId)
        AND (@SearchName IS NULL OR FullName LIKE '%' + @SearchName + '%')
        AND (@FromDate IS NULL OR TestCreatedAt >= @FromDate)
        AND (@ToDate IS NULL OR TestCreatedAt <= @ToDate)
    ORDER BY TestCreatedAt DESC;
END;
GO