
CREATE SCHEMA InjuriesData
GO


CREATE OR ALTER PROCEDURE InjuriesData.sp_GetInjuryDetailesById
(
    @Id UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        I.Id,
        I.Name,
        I.Description,
        I.InjuryType,
        I.AverageRecoveryTimeInDayes,

        COUNT(PI.Id) AS PlayerInjuriesCount,

        COUNT(DISTINCT PI.PlayerId) AS PlayeresInjuredCount,

        COUNT(
            CASE
                WHEN PI.Status = 0 THEN 1
            END
        ) AS CurrentlyInHealingCount

    FROM dbo.Injuries I

    LEFT JOIN dbo.PlayerInjuries PI
        ON PI.InjuryId = I.Id

    WHERE I.Id = @Id

    GROUP BY
        I.Id,
        I.Name,
        I.Description,
        I.InjuryType,
        I.AverageRecoveryTimeInDayes
END

InjuriesData.sp_GetInjuryDetailesById @Id = 'e380fb48-62e3-45a1-8417-e0916a0f54ac'