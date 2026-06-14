CREATE PROCEDURE tsa.sp_GetPlayerAdherenceOverTime
    @PlayerId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    -- Adherence = (Done Sessions / Total Sessions excluding Cancelled)
    -- Directly filtering and grouping using UserAccessPolicies table
    SELECT 
        FORMAT(uap.CreatedAt, 'MMMM yyyy') AS [Month],
        CAST(
            CASE 
                WHEN COUNT(CASE WHEN uap.AttendanceState <> 1 THEN 1 END) = 0 THEN 0
                ELSE ROUND(
                    (SUM(CASE WHEN uap.AttendanceState = 2 THEN 1.0 ELSE 0.0 END) / 
                     COUNT(CASE WHEN uap.AttendanceState <> 1 THEN 1 END)) * 100, 2
                )
            END AS DECIMAL(5,2)
        ) AS Adherence
    FROM 
        UserAccessPolicies uap
    WHERE 
        uap.UserId = @PlayerId -- PlayerId matches UserId directly
    GROUP BY 
        YEAR(uap.CreatedAt), 
        MONTH(uap.CreatedAt),
        FORMAT(uap.CreatedAt, 'MMMM yyyy')
    ORDER BY 
        YEAR(uap.CreatedAt) ASC, 
        MONTH(uap.CreatedAt) ASC;
END;
GO