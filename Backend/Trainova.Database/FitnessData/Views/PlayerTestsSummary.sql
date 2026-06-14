CREATE OR ALTER VIEW FitnessData.vw_PlayerTestsSummary
WITH SCHEMABINDING
AS
SELECT 
    -- CapacityTests Columns
    ct.Id AS CapacityTestId,
    ct.MaximumOxygenConsumption,
    ct.YoYoIntermittentRecoveryLevel1Distance,
    ct.YoYoIntermittentRecoveryLevel2Distance,
    ct.Time10Meters,
    ct.Time30Meters,
    ct.CountermovementJumpHeight,
    ct.ReactiveStrengthIndex,
    ct.CalculatedCapacity,
    ct.CreationType,
    ct.OverriddenCapacity,
    ct.ProgressFromLastTest,
    ct.CreatedAt AS TestCreatedAt,
    ct.CreatedBy AS TestCreatedBy,

    -- Players Columns
    p.PlayerNumber,
    p.TShirtName,
    p.MedicalStatus,
    p.CurrentMainPosition,
    p.OtherAvailablePositions,
    p.PerformanceLevel,
    p.DateOfEnrolment,
    p.CreatedAt AS PlayerCreatedAt,
    p.LastUpdate AS PlayerLastUpdate,

    -- Users Columns
    u.Id AS UserId,
    u.TeamId,
    u.ShowName,
    u.FullName,
    u.PhotoPath,
    u.Email,
    u.IsActive,
    u.Role,
    u.CreatedAt AS UserCreatedAt,
    u.LastUpdate AS UserLastUpdate
FROM dbo.CapacityTests ct
INNER JOIN dbo.Players p ON ct.PlayerId = p.Id
INNER JOIN dbo.Users u ON ct.PlayerId = u.Id;
GO

-- Create Unique Clustered Index to make it an Indexed View
CREATE UNIQUE CLUSTERED INDEX IX_vw_PlayerTestsSummary_CapacityTestId 
ON FitnessData.vw_PlayerTestsSummary (CapacityTestId);
GO