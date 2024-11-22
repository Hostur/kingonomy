using Kingonomy.Models;
using KingonomyService.DB;
using KingonomyService.DB.Queries;
using KingonomyService.Utils;
using Microsoft.Extensions.Caching.Distributed;

namespace KingonomyService.Services
{
    public sealed class ResourcesService
    {
        private readonly DBProvider _dbProvider;
        private readonly IDistributedCache _cache;
        public ResourcesService(DBProvider dbProvider, IDistributedCache cache)
        {
            _dbProvider = dbProvider;
            _cache = cache;
        }

        public async Task<ResourceModel[]> GetResources(int playerId)
        {
            string id = $"resources_{playerId}";
            var result = await _cache.GetRecordAsync<ResourceModel[]>(id);
            if (result == null)
            {
                var dbResult = await new SelectPlayerResourcesQuery(playerId).Execute();
                result = dbResult.ToArray();
                await _cache.SetRecordAsync(id, result, TimeSpan.MaxValue, TimeSpan.MaxValue);
            }

            return result;
        }

        public async Task<bool> AddPlayerResource(int playerId, string resourceId, float value)
        {
            bool result = await _dbProvider.AddPlayerResources(playerId, resourceId, value);
            if (result)
                await RefreshResourcesCache(playerId);

            return result;
        }

        public async Task<bool> IncreasePlayerResource(int playerId, string resourceId, float value)
        {
            bool result = await _dbProvider.IncreasePlayerResource(playerId, resourceId, value);
            if (result)
                await RefreshResourcesCache(playerId);

            return result;
        }

        public async Task<bool> DecreasePlayerResource(int playerId, string resourceId, float value)
        {
            bool result = await _dbProvider.DecreasePlayerResource(playerId, resourceId, value);
            if (result)
                await RefreshResourcesCache(playerId);

            return result;
        }

        private async Task RefreshResourcesCache(int playerId)
        {
            string id = $"resources_{playerId}";
            var dbResult = await new SelectPlayerResourcesQuery(playerId).Execute();
            await _cache.SetRecordAsync(id, dbResult.ToArray(), TimeSpan.MaxValue, TimeSpan.MaxValue);
        }
    }
}
