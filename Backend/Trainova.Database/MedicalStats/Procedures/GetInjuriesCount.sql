
CREATE OR ALTER PROCEDURE InjuriesData.sp_GetInjuriesCount
    @DaysCount INT = 7,
    @InjuryId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StartDate DATETIME = DATEADD(DAY, -@DaysCount, GETDATE());

    SELECT 
        -- الحالات النشطة حالياً (المصابين الذين لم يعودوا بعد)
        COUNT(CASE WHEN ReturnedAt IS NULL THEN 1 END) AS CurrentlyInjuredCount,

        -- 1. Active (InHealing)
        COUNT(CASE WHEN Status = 'InHealing' THEN 1 END) AS ActiveInHealing,
        COUNT(CASE WHEN Status = 'InHealing' AND CreatedAt >= @StartDate THEN 1 END) AS ActiveInHealingIncrease,

        -- 2. Recovered (Ended)
        COUNT(CASE WHEN Status = 'Ended' THEN 1 END) AS RecoveredEnded,
        COUNT(CASE WHEN Status = 'Ended' AND CreatedAt >= @StartDate THEN 1 END) AS RecoveredEndedIncrease,

        -- 3. In Recovery
        COUNT(CASE WHEN Status = 'InRecovery' THEN 1 END) AS InRecovery,
        COUNT(CASE WHEN Status = 'InRecovery' AND CreatedAt >= @StartDate THEN 1 END) AS InRecoveryIncrease,

        -- 4. New Injuries
        COUNT(CASE WHEN IsNew = 1 THEN 1 END) AS NewInjuries,
        COUNT(CASE WHEN IsNew = 1 AND CreatedAt >= @StartDate THEN 1 END) AS NewInjuriesIncrease,

        -- 5. Not New
        COUNT(CASE WHEN IsNew = 0 THEN 1 END) AS NotNewInjuries,
        COUNT(CASE WHEN IsNew = 0 AND CreatedAt >= @StartDate THEN 1 END) AS NotNewInjuriesIncrease,

        -- 6. Totals
        COUNT(*) AS TotalMonitoredCases,
        COUNT(CASE WHEN CreatedAt >= @StartDate THEN 1 END) AS TotalMonitoredIncrease

    FROM dbo.PlayerInjuries
    WHERE (@InjuryId IS NULL OR Id = @InjuryId);
END
GO