using KingonomyService.DB;
using Microsoft.Extensions.Caching.Distributed;

namespace KingonomyService.Services
{
    public sealed class ConfigurationService
    {
        private readonly DBProvider _dbProvider;
        private readonly IDistributedCache _cache;

        public ConfigurationService(DBProvider dbProvider, IDistributedCache cache)
        {
            _dbProvider = dbProvider;
            _cache = cache;
        }

        // Tutaj na razie notatki. Zrobiłbym kilka rodzajów enumów
        // 1. Kiedy coś powinno nastąpić np. OnLogin, OnFirstLoginToday, OnPlayerCreated, OnMatchResult, 
    }
}
