
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
                WHEN PI.Status = 'InHealing' THEN 1
            END
        ) AS CurrentlyInHealingCount,

        COUNT(
            CASE
                WHEN PI.Status = 'InRecovery' THEN 1
            END
        ) AS CurrentlyInRecoveryCount

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

InjuriesData.sp_GetInjuryDetailesById @Id = '3a96780c-1c44-4d73-aca8-8269dd6bb48c'