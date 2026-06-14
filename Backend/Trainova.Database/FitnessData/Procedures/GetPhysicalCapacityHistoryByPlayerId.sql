CREATE OR ALTER PROCEDURE FitnessData.sp_GetPhysicalCapacityHistoryByPlayerId
    @PlayerId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        PlayerId,
        CreationType,
        CreatedAt,
        CreatedBy,

        CalculatedCapacity,
        ProgressFromLastTest,
        OverriddenCapacity,

        MaximumOxygenConsumption,
        YoYoIntermittentRecoveryLevel1Distance,
        YoYoIntermittentRecoveryLevel2Distance,

        Time10Meters,
        Time30Meters,

        CountermovementJumpHeight,
        ReactiveStrengthIndex
    FROM 
        dbo.CapacityTests
    WHERE 
        PlayerId = @PlayerId
    ORDER BY 
        CreatedAt ASC;
END;
GO


FitnessData.sp_GetPhysicalCapacityHistoryByPlayerId 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab'