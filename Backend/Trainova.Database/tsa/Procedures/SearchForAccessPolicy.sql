IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'tsa')
BEGIN
    EXEC('CREATE SCHEMA tsa;');
END
GO



CREATE OR ALTER PROCEDURE tsa.sp_SearchForAccessPolicy
    @SearchTerm NVARCHAR(150) = NULL,
    @PageNumber INT = 0,
    @PageSize INT = 12
AS
BEGIN
    SET NOCOUNT ON;

    SET @SearchTerm = ISNULL(TRIM(@SearchTerm), '');

    SELECT 
        ap.Id,
        ap.PolicyName,
        ap.TYPE,
        ap.CreatedAt,
        ap.LastUpdate,
        COUNT(uap.UserId) AS AccessPolicyUsersCount 
    FROM 
        dbo.AccessPolicies ap 
    LEFT JOIN 
        dbo.UserAccessPolicies uap ON ap.Id = uap.AccessPoliciesId 
    WHERE 
        (@SearchTerm = '' OR ap.PolicyName LIKE '%' + @SearchTerm + '%')
    GROUP BY 
        ap.Id, 
        ap.PolicyName,
        ap.Type,
        ap.CreatedAt,
        ap.LastUpdate
    ORDER BY 
        ap.CreatedAt DESC 
    OFFSET (@PageNumber * @PageSize) ROWS
    FETCH NEXT (@PageSize) ROWS ONLY;
END
GO