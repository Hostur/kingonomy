using Kingonomy.Models;
using KingonomyService.DB;
using KingonomyService.DB.Queries;
using KingonomyService.Utils;
using Microsoft.Extensions.Caching.Distributed;

namespace KingonomyService.Services
{
    public sealed class ItemsService
    {
        private readonly DBProvider _dbProvider;
        private readonly IDistributedCache _cache;
        public ItemsService(DBProvider dbProvider, IDistributedCache cache)
        {
            _dbProvider = dbProvider;
            _cache = cache;
        }

        public async Task<bool> AddItemTemplate(string customId, string metadata)
        {
            var result = await _dbProvider.AddItemTemplate(customId, metadata);
            if(result)
                await RefreshItemTemplatesCache().ConfigureAwait(false);

            return result;
        }

        public async Task<bool> DeleteItemTemplate(string customId)
        {
            var result = await _dbProvider.DeleteItemTemplate(customId);
            if (result)
                await RefreshItemTemplatesCache().ConfigureAwait(false);

            return result;
        }

        public async Task<bool> DeleteItemTemplate(int id)
        {
            var result = await _dbProvider.DeleteItemTemplate(id);
            if (result)
                await RefreshItemTemplatesCache().ConfigureAwait(false);

            return result;
        }

        public async Task<ItemTemplateModel[]> GetItemTemplates()
        {
            const string id = "item_templates";
            var result = await _cache.GetRecordAsync<ItemTemplateModel[]>(id);
            if (result == null)
            {
                var dbResult = await new SelectItemTemplatesQuery().Execute();
                result = dbResult.ToArray();
                await _cache.SetRecordAsync(id, result, TimeSpan.MaxValue, TimeSpan.MaxValue);
            }

            return result;
        }

        public async Task<PlayerItemModel[]> GetItems(int playerId)
        {
            string id = $"items_{playerId}";
            var result = await _cache.GetRecordAsync<PlayerItemModel[]>(id);
            if (result == null)
            {
                var dbResult = await new SelectPlayerItemsQuery(playerId).Execute();
                result = dbResult.ToArray();
                await _cache.SetRecordAsync(id, result, TimeSpan.MaxValue, TimeSpan.MaxValue);
            }

            return result;
        }

        public async Task<bool> AddPlayerItem(int playerId, string customId, string metadata)
        {
            bool added = await _dbProvider.AddPlayerItem(playerId, customId, metadata).ConfigureAwait(false);
            if (added)
                await RefreshItemsCache(playerId);

            return added;
        }

        public async Task<bool> DeletePlayerItem(int playerId, int itemId)
        {
            bool deleted = await _dbProvider.DeletePlayerItem(playerId, itemId).ConfigureAwait(false);
            if(deleted)
                await RefreshItemsCache(playerId);
            return deleted;
        }

        public async Task<bool> ModifyPlayerItem(int playerId, int itemId, string metadata)
        {
            bool modified = await _dbProvider.ModifyPlayerItem(playerId, itemId, metadata).ConfigureAwait(false);
            if (modified)
                await RefreshItemsCache(playerId);

            return modified;
        }

        private async Task RefreshItemsCache(int playerId)
        {
            string id = $"items_{playerId}";
            var dbResult = await new SelectPlayerItemsQuery(playerId).Execute();
            await _cache.SetRecordAsync(id, dbResult.ToArray(), TimeSpan.MaxValue, TimeSpan.MaxValue);
        }

        private async Task RefreshItemTemplatesCache()
        {
            string id = $"item_templates";
            var dbResult = await new SelectItemTemplatesQuery().Execute();
            await _cache.SetRecordAsync(id, dbResult.ToArray(), TimeSpan.MaxValue, TimeSpan.MaxValue);
        }
    }
}
