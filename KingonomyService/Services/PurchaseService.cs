using Kingonomy.Models;
using KingonomyService.DB;
using KingonomyService.Utils;
using Microsoft.Extensions.Caching.Distributed;

namespace KingonomyService.Services
{
    public sealed class PurchaseService
    {
        private readonly DBProvider _dbProvider;
        private readonly IDistributedCache _cache;
        public PurchaseService(DBProvider dbProvider, IDistributedCache cache)
        {
            _dbProvider = dbProvider;
            _cache = cache;
        }

        public async Task<PurchaseModel[]> GetPurchaseModels()
        {
            var result = await _cache.GetRecordAsync<PurchaseModel[]>("purchase_models");
            if (result == null)
            {
                var dbResult = await _dbProvider.GetPurchaseModels().ConfigureAwait(false);
                result = dbResult.ToArray();
                await _cache.SetRecordAsync("purchase_models", result, TimeSpan.MaxValue, TimeSpan.MaxValue);
            }

            return result;
        }

        public async Task<bool> AddPurchaseModel(string id, string reward, string price)
        {
            var result = await _dbProvider.AddPurchaseModel(id, reward, price).ConfigureAwait(false);
            if (result)
                await RefreshPurchasesCache().ConfigureAwait(false);

            return result;
        }

        public async Task<bool> DeletePurchaseModel(string id)
        {
            var result = await _dbProvider.DeletePurchaseModel(id).ConfigureAwait(false);
            if (result)
                await RefreshPurchasesCache().ConfigureAwait(false);

            return result;
        }

        private async Task RefreshPurchasesCache()
        {
            var dbResult = await _dbProvider.GetPurchaseModels().ConfigureAwait(false);
            await _cache.SetRecordAsync("purchase_models", dbResult.ToArray(), TimeSpan.MaxValue, TimeSpan.MaxValue);
        }
    }
}
