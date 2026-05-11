CREATE SCHEMA PlayersData
GO



CREATE OR ALTER VIEW PlayersData.View_PlayerDetails
WITH SCHEMABINDING
AS
SELECT 
    p.Id,
    u.ShowName,
    u.FullName,
    u.Email,
    u.IsActive,
    u.PhotoPath,
    p.TShirtName,
    p.PlayerNumber,
    p.PerformanceLevel,
    p.CurrentMainPosition, -- Flagged Enum
    p.OtherAvailablePositions, -- Flagged Enum
    p.MedicalStatus,
    p.DateOfEnrolment,
    p.CreatedAt
FROM dbo.Players p
JOIN dbo.Users u ON p.Id = u.Id


GO

CREATE UNIQUE CLUSTERED index IX_View_PlayerInjury_Player_Injury_Id on PlayersData.View_PlayerDetails(Id);
