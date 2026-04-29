namespace Trainova.Api.Models
{
    public record ApiResponse<T>(
        T? Data,
        string? Message = null,
        int StatusCode = 200,
        DateTime ResponseTime = default,
        string? ValueType = null,
        int? Count = null,
        int? TotalCount = null,
        Guid? UserId = null
    );
}
