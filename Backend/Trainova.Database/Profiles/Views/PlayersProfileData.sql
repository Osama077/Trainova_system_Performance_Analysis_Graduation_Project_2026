CREATE SCHEMA PlayersData
GO



CREATE OR ALTER VIEW PlayersData.View_PlayerDetails AS
SELECT 
    p.Id,
    u.ShowName,
    u.FullName,
    u.Email,
    u.IsActive,
    u.PhotoPath,
    u.TeamId,
    p.TShirtName,
    p.PlayerNumber,
    p.PerformanceLevel,
    p.CurrentMainPosition, -- Flagged Enum
    p.OtherAvailablePositions, -- Flagged Enum
    p.MedicalStatus,
    p.DateOfEnrolment,
    p.CreatedAt,
    COUNT(DISTINCT l.Id) AS MatchesCount,
    COUNT(DISTINCT pi.Id) AS InjuriesCount
FROM dbo.Players p
JOIN dbo.Users u ON p.Id = u.Id
LEFT JOIN dbo.Lineups l ON p.Id = l.PlayerId
LEFT JOIN dbo.PlayerInjuries pi ON p.Id = pi.PlayerId
GROUP BY 
    p.Id, u.ShowName, u.FullName, u.Email, u.IsActive, u.PhotoPath, 
    u.TeamId, p.TShirtName, p.PlayerNumber, p.PerformanceLevel, 
    p.CurrentMainPosition, p.OtherAvailablePositions, p.MedicalStatus, 
    p.DateOfEnrolment, p.CreatedAt;
GO

