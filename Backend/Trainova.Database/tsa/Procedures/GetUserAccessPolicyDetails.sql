CREATE OR ALTER PROCEDURE tsa.sp_GetUserAccessPolicyDetails
    @PolicyId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        uap.Id,
        uap.AttendanceState AS AttendanceStatus,
        uap.DoneScore,
        uap.CreatedAt,
        uap.LastUpdate,

        u.Id AS UserId,
        u.ShowName AS UserShowName,
        u.FullName,
        u.PhotoPath,

        ap.Id AS AccessPolicyId,
        ap.PolicyName AS AccessPolicyName
    FROM 
        dbo.UserAccessPolicies uap
    INNER JOIN 
        dbo.Users u ON uap.UserId = u.Id
    INNER JOIN 
        dbo.AccessPolicies ap ON uap.AccessPoliciesId = ap.Id
    WHERE 
        ap.Id = @PolicyId;
END
GO