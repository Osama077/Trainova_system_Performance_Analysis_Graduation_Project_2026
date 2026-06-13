namespace Trainova.Application.Common.Interfaces.Services
{
    public interface ICacheService<TValue>
    {
        Task<TValue?> GetAsync(string cacheKey);
        Task SetAsync<TValue>(string cacheKey, TValue? value, TimeSpan? expiration);
    }
}
