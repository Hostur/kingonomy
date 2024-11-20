using Kingonomy.Models;
using KingonomyService.DB;

namespace KingonomyService.Services
{
    public sealed class ItemsService
    {
        private readonly ILogger<ItemsService> _logger;
        private readonly DBProvider _dbProvider;

        public ItemsService(ILogger<ItemsService> logger, DBProvider dbProvider)
        {
            _logger = logger;
            _dbProvider = dbProvider;
        }

        public async Task<List<ItemTemplateModel>> GetItemTemplates() => await _dbProvider.GetItemTemplates().ConfigureAwait(false);
        public async Task<List<PlayerItemModel>> GetItems(int playerId) => await _dbProvider.GetItems(playerId).ConfigureAwait(false);
    }
}
