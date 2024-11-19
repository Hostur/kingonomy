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

        public async Task<ItemsModel> GetItems()
        {
            var items = await _dbProvider.GetItems().ConfigureAwait(false);
            return new ItemsModel(items.ToArray());
        }
    }
}
