using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Trainova.Api.Filters
{
    public class IdempotencyHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasFilter = context.ApiDescription.ActionDescriptor.EndpointMetadata
                        .Any(m => (m is ServiceFilterAttribute sfa && sfa.ServiceType == typeof(IdempotencyFilter)) ||
                                  (m is TypeFilterAttribute tfa && tfa.ImplementationType == typeof(IdempotencyFilter)));
            if (!hasFilter) return;

            if (operation.Parameters == null)
                operation.Parameters = new List<IOpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-Idempotency-Key",
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = JsonSchemaType.String,
                    Format = "uuid"
                },
                Description = "Unique request identifier (GUID) to ensure idempotency."
            });
        }
    }
}
