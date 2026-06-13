namespace Trainova.Infrastructure.Cacheing
{
    public class CacheIntegrationOptions
    {
        public string Domain { get; set; } = string.Empty;
        public string Port { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public bool UseSSL { get; set; }

        public string Link => $"{(UseSSL ? "https" : "http")}://{Domain}:{Port}/";

        public string RedisConnectionString => $"{Domain}:{Port},password={UserName},ssl={UseSSL}";
    }

    public enum TargetedCacheType
    {
        InMemory,
        Redis
    }
}
