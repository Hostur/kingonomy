using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace KingonomyService.Utils
{
    public static class DistributedCacheExtension
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache,
            string recordId,
            T data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpireTime = null)
        {
            var option = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60),
                SlidingExpiration = unusedExpireTime
            };

            var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recordId, jsonData, option);
        }

        public static async Task SetRecordAsync(this IDistributedCache cache,
            string recordId,
            string data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpireTime = null)
        {
            var option = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60),
                SlidingExpiration = unusedExpireTime
            };

            await cache.SetStringAsync(recordId, data, option);
        }

        public static async Task<T?> GetRecordAsync<T>(this IDistributedCache cache, string recordId) 
        {
            var jsonData = await cache.GetStringAsync(recordId);
            return jsonData is null ? default(T) : JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
