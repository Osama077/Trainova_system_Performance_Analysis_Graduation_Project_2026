CREATE OR ALTER PROCEDURE FitnessData.sp_GetTeamPlayersFitnessGrid
    @SearchName NVARCHAR(200) = NULL,
    @Position INT = NULL, -- Changed to INT for Smart Enum matching
    @FootageStatus NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.Id AS PlayerId,
        u.FullName AS PlayerName,
        u.PhotoPath,
        p.PlayerNumber,
        p.CurrentMainPosition AS MainPosition, -- Will return the INT value directly
        p.MedicalStatus,
        
        -- Latest Physical Capacity Test Details
        LatestTest.CalculatedCapacity,
        LatestTest.OverriddenCapacity,
        LatestTest.ProgressFromLastTest,
        LatestTest.TestCreatedAt AS LatestTestDate,

        -- Latest Session Movement Details
        LatestSession.SprintsCount,
        LatestSession.DurationInMinutes,
        LatestSession.PlayerCalculatedLoad,
        LatestSession.OverriddenLoad,
        LatestSession.LoadRatioFromLastSession,
        LatestSession.FootageLoadToCapacityRatio,
        ISNULL(LatestSession.FootageStatus, N'Baseline Status') AS FootageStatus,
        LatestSession.TotalDistance,
        LatestSession.SessionCreatedAt AS LatestSessionDate
    FROM dbo.Players p
    INNER JOIN dbo.Users u ON p.Id = u.Id
    
    -- Subquery to get the strictly single latest capacity test
    OUTER APPLY (
        SELECT TOP 1 CalculatedCapacity, OverriddenCapacity, ProgressFromLastTest, TestCreatedAt
        FROM FitnessData.vw_PlayerTestsSummary
        WHERE UserId = p.Id
        ORDER BY TestCreatedAt DESC
    ) LatestTest
    
    -- Subquery to get the strictly single latest session movement
    OUTER APPLY (
        SELECT TOP 1 SprintsCount, DurationInMinutes, PlayerCalculatedLoad, OverriddenLoad, 
                     LoadRatioFromLastSession, FootageLoadToCapacityRatio, FootageStatus, TotalDistance, SessionCreatedAt
        FROM FitnessData.vw_PlayerSessionsSummary
        WHERE UserId = p.Id
        ORDER BY SessionCreatedAt DESC
    ) LatestSession
    
    WHERE (@SearchName IS NULL OR u.FullName LIKE '%' + @SearchName + '%')
      AND (@Position IS NULL OR p.CurrentMainPosition = @Position)
      AND (@FootageStatus IS NULL OR LatestSession.FootageStatus = @FootageStatus)
      
    ORDER BY p.CurrentMainPosition DESC, u.FullName ASC;
END;
GO