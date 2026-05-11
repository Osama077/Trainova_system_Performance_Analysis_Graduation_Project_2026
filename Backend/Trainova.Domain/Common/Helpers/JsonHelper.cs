using System.Text.Json;
using System.Text.Json.Serialization;
using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.Common.BaseEntity;
using Trainova.Domain.UserAuth;

namespace Trainova.Domain.Common.Helpers
{
    public static class JsonHelper
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
            Converters =
            {
                new JsonStringEnumConverter(),
                new SmartEnumJsonConverter<TokenType>(),
            }

        };


        // Serialize any object
        public static string Serialize<T>(this T value)
            where T : IAuditable
        {
            if (value is null)
                return string.Empty;

            return JsonSerializer.Serialize(
                value,
                value.GetType(),
                _options
            );
        }


        // Deserialize into any type
        public static T? Deserialize<T>(this string? json)
            where T : ICreatorLogable
        {
            if (string.IsNullOrWhiteSpace(json))
                return default;

            return JsonSerializer.Deserialize<T>(json, _options);
        }
    }
}
