using Kingonomy.Models;
using KingonomyService.DB.Queries;

namespace KingonomyService.DB
{
    public sealed class DBProvider
    {
        #region Items

        public async Task<List<ItemModel>> GetItems()
        {
            var query = new SelectItemsQuery();
            return await query.Execute().ConfigureAwait(false);
        }

        /// <summary>
        /// Add new item to player.
        /// </summary>
        /// <param name="playerId">Player serial id.</param>
        /// <param name="itemId">Item template serial id.</param>
        /// <param name="metadata">Item metadata, can be copied from template.</param>
        /// <returns>Value indicating whether operation succeed.</returns>
        public async Task<bool> AddPlayerItem(int playerId, int itemId, string metadata)
        {
            var query = new AddPlayerItemQuery(playerId, itemId, metadata);
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

        public async Task<List<ResourceModel>> GetResources()
        {
            var query = new SelectResourcesQuery();
            return await query.Execute().ConfigureAwait(false);
        }

        /// <summary>
        /// Add all the resources that player doesn't have yet to the player.
        /// Kind of migration.
        /// </summary>
        /// <returns>Value indicating whether operation succeed.</returns>
        public async Task<bool> AddPlayerResources(int playerId)
        {
            var query = new AddPlayerResourcesQuery(playerId);
            return await query.Execute().ConfigureAwait(false);
        }

        public async Task<bool> IncreasePlayerResource(int playerId, int itemId, float value)
        {
            var query = new ModifyResourceValueQuery(playerId, itemId, MathF.Abs(value));
            return await query.Execute().ConfigureAwait(false);
        }

        public async Task<bool> DecreasePlayerResource(int playerId, int itemId, float value)
        {
            var query = new ModifyResourceValueQuery(playerId, itemId, -MathF.Abs(value));
            return await query.Execute().ConfigureAwait(false);
        }
        #endregion
    }
}
