using Kingonomy;
using Kingonomy.Models;
using KingonomyService.DB.Development;
using KingonomyService.DB.Queries;

namespace KingonomyService.DB
{
    public sealed class DBProvider
    {
        #region Items

        public async Task<List<PlayerItemModel>> GetItems(int playerId)
        {
            var query = new SelectPlayerItemsQuery(playerId);
            return await query.Execute().ConfigureAwait(false);
        }

        public async Task<List<PlayerItemModel>> GetItems(string playerUnityId)
        {
            var query = new SelectPlayerItemsQuery(playerUnityId);
            return await query.Execute().ConfigureAwait(false);
        }

        public async Task<bool> AddPlayerItem(int playerId, string customId, string metadata)
        {
            var query = new AddPlayerItemQuery(playerId, customId, metadata);
            return await query.Execute().ConfigureAwait(false);
        }

        public async Task<bool> DeletePlayerItem(int playerId, int itemId)
        {
            var query = new DeletePlayerItemQuery(playerId, itemId);
            return await query.Execute().ConfigureAwait(false);
        }

        public async Task<bool> ModifyPlayerItem(int playerId, int itemId, string metadata)
        {
            var query = new ModifyPlayerItemQuery(playerId, itemId, metadata);
            return await query.Execute().ConfigureAwait(false);
        }
        #endregion

        #region Resources

        public async Task<List<ResourceModel>> GetResources(int playerId)
        {
            var query = new SelectPlayerResourcesQuery(playerId);
            return await query.Execute().ConfigureAwait(false);
        }

        public async Task<List<ResourceModel>> GetResources(string playerUnityId)
        {
            var query = new SelectPlayerResourcesQuery(playerUnityId);
            return await query.Execute().ConfigureAwait(false);
        }

        public async Task<bool> AddPlayerResources(int playerId, string resourceId, float value)
        {
            var query = new AddPlayerResourceQuery(playerId, resourceId, value);
            return await query.Execute().ConfigureAwait(false);
        }

        public async Task<bool> IncreasePlayerResource(int playerId, string itemId, float value)
        {
            var query = new ModifyResourceValueQuery(playerId, itemId, MathF.Abs(value));
            return await query.Execute().ConfigureAwait(false);
        }

        public async Task<bool> DecreasePlayerResource(int playerId, string itemId, float value)
        {
            var query = new ModifyResourceValueQuery(playerId, itemId, -MathF.Abs(value));
            return await query.Execute().ConfigureAwait(false);
        }
        #endregion

        #region Purchases

        public async Task<bool> AddPurchaseModel(string id, string reward, string price)
        {
            var query = new AddPurchaseModelQuery(id, reward, price);
            return await query.Execute().ConfigureAwait(false);
        }

        public async Task<List<PurchaseModel>> GetPurchaseModels()
        {
            var query = new SelectPurchaseModelsQuery();
            return await query.Execute().ConfigureAwait(false);
        }

        public async Task<bool> DeletePurchaseModel(string id)
        {
            var query = new DeletePurchaseModelQuery(id);
            return await query.Execute().ConfigureAwait(false);
        }

        #endregion

        #region Users
        public async Task<PlayerModel?> AddPlayer(string unityId, Role role, string metadata)
        {
            var query = new AddPlayerQuery(unityId, role, metadata);
            return await query.Execute().ConfigureAwait(false);
        }
        #endregion
    }
}
