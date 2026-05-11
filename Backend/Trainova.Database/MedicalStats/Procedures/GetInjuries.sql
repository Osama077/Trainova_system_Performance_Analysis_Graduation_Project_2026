CREATE OR ALTER PROCEDURE InjuriesData.sp_GetInjuries
    @Id UNIQUEIDENTIFIER = NULL,
    @InjuryType NVARCHAR(50) = NULL,
    @SearchTerm NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM dbo.Injuries
    WHERE (@Id IS NULL OR Id = @Id)
      AND (@InjuryType IS NULL OR InjuryType = @InjuryType)
      AND (@SearchTerm IS NULL OR ([Description] LIKE '%' + @SearchTerm + '%' OR InjuryType LIKE '%' + @SearchTerm + '%'))
    ORDER BY CreatedAt DESC;
END