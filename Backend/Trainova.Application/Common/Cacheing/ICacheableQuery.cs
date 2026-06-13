namespace Trainova.Application.Common.Cacheing
{
    public interface ICacheableQuery
    {
        string? CacheKeyPrefix { get; }
        TimeSpan? Expiration { get; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CacheKeyParameterAttribute : Attribute
    {
        public string? KeyName { get; set; }

        public CacheKeyParameterAttribute(string? keyName = null)
        {
            KeyName = keyName;
        }
    }
}