using System.Text.Json;
using System.Text.Json.Serialization;
using Trainova.Domain.Common.DataConvrters;
using Trainova.Domain.UserAuth.UserTokens;

namespace Trainova.Domain.Common.Helpers
{
    public static class JsonHelper
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false,
            Converters =
            {
                new JsonStringEnumConverter(),              // Enums العادية
                new SmartEnumJsonConverter<TokenType>(),  // SmartEnums اللي محتاجها
            }
        };


        // Serialize any object
        public static string Serialize<T>(this T value)
        {
            return JsonSerializer.Serialize(value, _options);
        }

        // Deserialize into any type
        public static T? Deserialize<T>(this string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return default;

            return JsonSerializer.Deserialize<T>(json, _options);
        }
    }
}
