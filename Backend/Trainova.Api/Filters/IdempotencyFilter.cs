using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Trainova.Infrastructure.DataAccess.IdempotencyModel;

namespace Trainova.Api.Filters;

public class IdempotencyFilter : IAsyncActionFilter
{
    private readonly IdempotencyDbContext _dbContext;
    private const string IdempotencyHeader = "X-Idempotency-Key";

    public IdempotencyFilter(IdempotencyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(IdempotencyHeader, out var headerValue) ||
            !Guid.TryParse(headerValue, out var requestId))
        {
            context.Result = new BadRequestObjectResult(new { code = "NotFound.Idempotency", Message = $"Missing or invalid {IdempotencyHeader} header." });
            return;
        }

        var existingRequest = await _dbContext.IdempotentRequests
            .FirstOrDefaultAsync(r => r.RequestId == requestId);

        if (existingRequest != null)
        {
            context.Result = new ContentResult
            {
                StatusCode = existingRequest.StatusCode,
                Content = existingRequest.ResponseBody,
                ContentType = "application/json"
            };
            return;
        }

        var executedContext = await next();

        if (executedContext.Result is ObjectResult objectResult && executedContext.HttpContext.Response.StatusCode >= 200 && executedContext.HttpContext.Response.StatusCode < 300)
        {
            var responseBody = System.Text.Json.JsonSerializer.Serialize(objectResult.Value);

            var idempotentRequest = new IdempotentRequest
            {
                RequestId = requestId,
                Name = $"{context.HttpContext.Request.Path}-{context.HttpContext.Request.Method}",
                StatusCode = executedContext.HttpContext.Response.StatusCode,
                ResponseBody = responseBody
            };

            _dbContext.IdempotentRequests.Add(idempotentRequest);
            await _dbContext.SaveChangesAsync();
        }
    }
}
