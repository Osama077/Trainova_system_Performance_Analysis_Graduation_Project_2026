using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Trainova.Infrastructure.DataAccess.IdempotencyModel
{
    public class IdempotencyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string IdempotencyHeader = "X-Idempotency-Key";

        public IdempotencyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IdempotencyDbContext dbContext)
        {
            if (!HttpMethods.IsPost(context.Request.Method))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue(IdempotencyHeader, out var headerValue) ||
                !Guid.TryParse(headerValue, out var requestId))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync($"Missing or invalid {IdempotencyHeader} header.");
                return;
            }

            var existingRequest = await dbContext.IdempotentRequests
                .FirstOrDefaultAsync(r => r.RequestId == requestId);

            if (existingRequest != null)
            {
                context.Response.StatusCode = existingRequest.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(existingRequest.ResponseBody);
                return;
            }

            var originalBodyStream = context.Response.Body;
            using var responseBodyMemoryStream = new MemoryStream();
            context.Response.Body = responseBodyMemoryStream;

            try
            {
                await _next(context);

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                var idempotentRequest = new IdempotentRequest
                {
                    RequestId = requestId,
                    Name = $"{context.Request.Path}-{context.Request.Method}",
                    StatusCode = context.Response.StatusCode,
                    ResponseBody = responseBody
                };

                dbContext.IdempotentRequests.Add(idempotentRequest);
                await dbContext.SaveChangesAsync();

                await responseBodyMemoryStream.CopyToAsync(originalBodyStream);
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }
    }

}
