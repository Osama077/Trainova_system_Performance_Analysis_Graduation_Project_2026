CREATE OR ALTER VIEW FitnessData.vw_PlayerSessionsSummary
WITH SCHEMABINDING
AS
SELECT 
    -- SessionMovements Columns
    sm.Id AS SessionMovementId,
    sm.SprintsCount,
    sm.TotalDistance,
    sm.WalkDistance,
    sm.RunDistance,
    sm.HighSpeedRunDistance,
    sm.AverageSpeed,
    sm.MaxSpeed,
    sm.PeakAcceleration,
    sm.DurationInMinutes,
    sm.FootageLoadToCapacityRatio,
    sm.FootageStatus,
    sm.LoadRatioFromLastSession,
    sm.OverriddenLoad,
    sm.PlayerCalculatedLoad,
    sm.CreatedAt AS SessionCreatedAt,
    sm.LastUpdate AS SessionLastUpdate,

    -- UserAccessPolicies Columns
    uap.Id AS UserAccessPolicyId,
    uap.AccessPoliciesId,
    uap.AttendanceState,
    uap.DoneScore,
    uap.CreatedAt AS PolicyCreatedAt,
    uap.LastUpdate AS PolicyLastUpdate,

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
    u.LastUpdate AS UserLastUpdate,

    -- Players Columns
    p.Id AS PlayerId,
    p.PlayerNumber,
    p.TShirtName,
    p.MedicalStatus,
    p.CurrentMainPosition,
    p.PerformanceLevel,
    p.CreatedAt AS PlayerCreatedAt,
    p.LastUpdate AS PlayerLastUpdate
FROM dbo.SessionMovements sm
INNER JOIN dbo.UserAccessPolicies uap ON sm.UserAccessPolicyId = uap.Id
INNER JOIN dbo.Users u ON uap.UserId = u.Id
INNER JOIN dbo.Players p ON u.Id = p.Id;
GO

-- Create Unique Clustered Index to make it an Indexed View
CREATE UNIQUE CLUSTERED INDEX IX_vw_PlayerSessionsSummary_SessionMovementId 
ON FitnessData.vw_PlayerSessionsSummary (SessionMovementId);
GO