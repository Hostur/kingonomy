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

        public async Task<bool> AddPlayerItem(int playerId, ItemModel model)
        {
            if (model.IsStackable)
            {
                var items = await GetItems(playerId);
                var item = items.FirstOrDefault(s => s.Item.Id == model.Id);
                if (item != null)
                {
                    item.Item.Quantity += model.Quantity;
                    return await ModifyPlayerItem(playerId, item.PlayerItemId, item.Item).ConfigureAwait(false);
                }
                else
                {
                    bool added = await _dbProvider.AddPlayerItem(playerId, model).ConfigureAwait(false);
                    if (added)
                        await RefreshItemsCache(playerId);

                    return added;
                }
            }
            else
            {
                bool added = await _dbProvider.AddPlayerItem(playerId, model).ConfigureAwait(false);
                if (added)
                    await RefreshItemsCache(playerId);

                return added;
            }
        }

        public async Task<bool> DeletePlayerItem(int playerId, int itemId)
        {
            bool deleted = await _dbProvider.DeletePlayerItem(playerId, itemId).ConfigureAwait(false);
            if(deleted)
                await RefreshItemsCache(playerId);
            return deleted;
        }

        public async Task<bool> ModifyPlayerItem(int playerId, int itemId, ItemModel model)
        {
            bool modified = await _dbProvider.ModifyPlayerItem(playerId, itemId, model.Quantity, model.MetaData).ConfigureAwait(false);
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
    }
}
