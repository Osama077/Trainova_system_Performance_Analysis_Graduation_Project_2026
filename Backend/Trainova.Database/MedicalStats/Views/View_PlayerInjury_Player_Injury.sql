-- View: View_PlayerInjury_Player_Injury
-- Joins PlayerInjuries with Players, Injuries and Users to provide a read-friendly projection
IF OBJECT_ID('dbo.View_PlayerInjury_Player_Injury', 'V') IS NOT NULL
DROP VIEW dbo.View_PlayerInjury_Player_Injury;
GO

CREATE Or Alter VIEW dbo.View_PlayerInjury_Player_Injury
        with SCHEMABINDING
AS
SELECT
    pi.Id AS PlayerInjuryId,
    pi.CreatedAt AS PlayerInjuryCreatedAt,
    pi.CreatedBy AS PlayerInjuryCreatedBy,
    p.Id AS PlayerId,
    p.PlayerNumber,
    p.TShirtName,
    p.MedicalStatus AS PlayerMedicalStatus,
    p.DateOfEnrolment,
    u.ShowName AS UserShowName,
    u.FullName AS UserFullName,
    u.Email AS UserEmail,
    i.Id AS InjuryId,
    i.Name AS InjuryName,
    i.Description AS InjuryDescription,
    i.InjuryType AS InjuryType,
    i.AverageRecoveryTime AS InjuryAverageRecoveryTime,
    pi.Status AS Status,
    pi.SevertiyGrade AS SevertiyGrade,
    pi.Cause AS Cause,
    pi.BodyPart,
    pi.Notes,
    pi.HappendAt,
    pi.ExpectedReturnDate,
    pi.ReturnedAt,
    pi.IsNew,
    COUNT_BIG(*) AS TotalCount
FROM dbo.PlayerInjuries pi
         INNER JOIN dbo.Players p ON p.Id = pi.PlayerId
         INNER JOIN dbo.Injuries i ON i.Id = pi.InjuryId
         INNER JOIN dbo.Users u ON u.Id = p.Id
GROUP BY
    pi.Id,
    pi.CreatedAt,
    pi.CreatedBy,
    p.Id,
    p.PlayerNumber,
    p.TShirtName,
    p.MedicalStatus,
    p.DateOfEnrolment,
    u.ShowName,
    u.FullName,
    u.Email,
    i.Id,
    i.Name,
    i.Description,
    i.InjuryType,
    i.AverageRecoveryTime,
    pi.Status,
    pi.SevertiyGrade,
    pi.Cause,
    pi.BodyPart,
    pi.Notes,
    pi.HappendAt,
    pi.ExpectedReturnDate,
    pi.ReturnedAt,
    pi.IsNew;
GO

-- Note: Creating an indexed view requires SCHEMABINDING and additional restrictions; keep index creation separate or create indexes on underlying tables if needed.
GO
create unique clustered index IX_View_PlayerInjury_Player_Injury_Id on dbo.View_PlayerInjury_Player_Injury(PlayerInjuryId);

