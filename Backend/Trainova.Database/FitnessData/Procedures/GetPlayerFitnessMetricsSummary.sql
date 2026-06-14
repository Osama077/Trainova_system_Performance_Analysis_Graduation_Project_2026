CREATE OR ALTER PROCEDURE FitnessData.sp_GetPlayerFitnessMetricsSummary
    @PlayerId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    -- Local variables to hold team statistics
    DECLARE @TeamId UNIQUEIDENTIFIER;
    SELECT TOP 1 @TeamId = TeamId FROM dbo.Users WHERE Id = @PlayerId;

    -- Baselines identical to Domain logic
    DECLARE @BaselineVO2Max DECIMAL(18,4) = 60.0;
    DECLARE @Baseline10m DECIMAL(18,4) = 1.75;
    DECLARE @Baseline30m DECIMAL(18,4) = 4.0;
    DECLARE @BaselineJump DECIMAL(18,4) = 45.0;
    DECLARE @BaselineRSI DECIMAL(18,4) = 2.0;

    -- Temp table to calculate scores for all team players to easily grab averages
    ;WITH RawMetrics AS (
        SELECT 
            u.Id AS PlayerId,
            ISNULL(ct.OverriddenCapacity, ct.CalculatedCapacity) AS OverallCap,
            ct.ProgressFromLastTest AS CapTrend,
            ISNULL(sm.OverriddenLoad, sm.PlayerCalculatedLoad) AS CurrentLoad,
            sm.LoadRatioFromLastSession AS LoadTrend,
            
            -- Aerobic Score
            (ct.MaximumOxygenConsumption / @BaselineVO2Max) * 100.0 AS Endurance,
            
            -- Speed Score
            CASE WHEN ct.Time10Meters > 0 AND ct.Time30Meters > 0 
                 THEN ((@Baseline10m / ct.Time10Meters) + (@Baseline30m / ct.Time30Meters)) / 2.0 * 100.0 
                 ELSE 0 END AS Speed,
            
            -- Power Score
            CASE WHEN ct.CountermovementJumpHeight > 0 AND ct.ReactiveStrengthIndex > 0 
                 THEN ((ct.CountermovementJumpHeight / @BaselineJump) + (ct.ReactiveStrengthIndex / @BaselineRSI)) / 2.0 * 100.0 
                 ELSE 0 END AS Power
        FROM dbo.Users u
        INNER JOIN FitnessData.vw_PlayerTestsSummary ct ON u.Id = ct.UserId
        -- Grab the latest movement session for trends
        OUTER APPLY (
            SELECT TOP 1 OverriddenLoad, PlayerCalculatedLoad, LoadRatioFromLastSession
            FROM FitnessData.vw_PlayerSessionsSummary
            WHERE UserId = u.Id
            ORDER BY SessionCreatedAt DESC
        ) sm
        WHERE u.TeamId = @TeamId
    )
    SELECT 
        -- Player Specific Scores
        p.Speed AS SpeedScore,
        p.Endurance AS EnduranceScore,
        p.Power AS ExplosivePowerScore,
        -- Your custom dynamic performance: (Capacity Progress / Load Progress)
        CASE WHEN p.LoadTrend > 0 THEN ROUND((p.CapTrend / p.LoadTrend) * 100.0, 2) ELSE 100.0 END AS FitnessPerformance,
        p.OverallCap AS OverallCapacity,

        -- Squad Averages
        ROUND(AVG(p2.OverallCap), 2) AS SquadAverageCapacity,
        ROUND(AVG(p2.Speed), 2) AS SquadAverageSpeed,
        ROUND(AVG(p2.Endurance), 2) AS SquadAverageEndurance,
        ROUND(AVG(p2.Power), 2) AS SquadAveragePower,
        ROUND(AVG(CASE WHEN p2.LoadTrend > 0 THEN (p2.CapTrend / p2.LoadTrend) * 100.0 ELSE 100.0 END), 2) AS SquadAveragePerformance
    FROM RawMetrics p
    CROSS JOIN RawMetrics p2
    WHERE p.PlayerId = @PlayerId
    GROUP BY p.Speed, p.Endurance, p.Power, p.CapTrend, p.LoadTrend, p.OverallCap;
END;
GO