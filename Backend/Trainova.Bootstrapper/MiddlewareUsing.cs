using Microsoft.AspNetCore.Builder;
using Trainova.Infrastructure.DataAccess.IdempotencyModel;

namespace Trainova.Bootstrapper
{
    public static class IdempotencyMiddlewareExtensions
    {
        public static IApplicationBuilder UseIdempotency(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IdempotencyMiddleware>();
        }
    }
}
