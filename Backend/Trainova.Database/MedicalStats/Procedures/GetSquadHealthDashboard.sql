CREATE OR ALTER PROCEDURE InjuriesData.sp_GetSquadHealthDashboard
    @Position INT = NULL,
    @InjuryStatus NVARCHAR(50) = NULL,
    @SeverityGrade INT  = NULL,
    @SearchName NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        u.[Id] AS PlayerId, u.[ShowName], u.[FullName], u.[PhotoPath], u.[Email],
        p.[PlayerNumber], p.[TShirtName], p.[MedicalStatus] AS PlayerMedicalStatus, p.[CurrentMainPosition], p.[OtherAvailablePositions], p.[PerformanceLevel], p.[DateOfEnrolment],
        pi.Id AS PlayerInjuryId, pi.[Status] AS InjuryStatus, pi.[Cause], pi.[SevertiyGrade], pi.[BodyPart], pi.[Notes], pi.[IsNew], pi.[HappendAt], pi.[ReturnedAt], pi.[ExpectedReturnDate],
        i.[Id] AS InjuryId, i.[Name] AS InjuryName, i.AverageRecoveryTimeInDayes, i.[Description] AS InjuryDescription, i.InjuryType,
        ISNULL(PhasesData.AvgProgress, 0) AS ProgressPercentage
    FROM Players p
    INNER JOIN Users u ON u.Id = p.Id
    LEFT JOIN PlayerInjuries pi ON p.Id = pi.PlayerId AND pi.Status != 'Ended' 
    LEFT JOIN Injuries i ON pi.InjuryId = i.Id
    OUTER APPLY (
        SELECT 
            AVG(CASE 
                WHEN GETUTCDATE() >= pp.[To] THEN 100.0 
                WHEN GETUTCDATE() <= pp.[From] THEN 0.0 
                ELSE (DATEDIFF(SECOND, pp.[From], GETUTCDATE()) * 100.0) / 
                     NULLIF(DATEDIFF(SECOND, pp.[From], pp.[To]), 0)
            END) AS AvgProgress
        FROM RecoveryPlanPhases pp
        WHERE pp.PlayerInjuryId = pi.Id
    ) AS PhasesData 
    WHERE 
        u.IsActive = 1 
        AND ((@Position IS NULL OR (p.CurrentMainPosition & @Position) > 0)
        OR (@Position IS NULL OR (p.OtherAvailablePositions & @Position) > 0))
        AND (@InjuryStatus IS NULL OR pi.Status = @InjuryStatus)
        AND (@SeverityGrade IS NULL OR pi.SevertiyGrade = @SeverityGrade)
        AND (
            @SearchName IS NULL 
            OR p.TShirtName LIKE '%' + @SearchName + '%' 
            OR u.FullName LIKE '%' + @SearchName + '%'
        )
    ORDER BY p.CurrentMainPosition, u.ShowName;
END