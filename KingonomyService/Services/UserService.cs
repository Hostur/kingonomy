using Kingonomy;
using Kingonomy.Models;
using KingonomyService.DB;
using KingonomyService.DB.Queries;
using KingonomyService.Utils;
using Microsoft.Extensions.Caching.Distributed;

namespace KingonomyService.Services
{
    public sealed class UserService
    {
        private readonly DBProvider _dbProvider;
        private readonly IDistributedCache _cache;
        private readonly UnityAuthorizationService _authorizationService;

        public UserService(DBProvider dbProvider, IDistributedCache cache, UnityAuthorizationService authorizationService)
        {
            _dbProvider = dbProvider;
            _cache = cache;
            _authorizationService = authorizationService;
        }

        //public async Task<PlayerModel?> TryToGetUserFromCache(string accessToken)
        //{
        //    string id = $"access_{accessToken}";
        //    return await _cache.GetRecordAsync<PlayerModel>(id);
        //}

        public async Task<PlayerModel?> TryToGetUserFromCache(string kingonomyToken) => await _cache.GetRecordAsync<PlayerModel>(kingonomyToken);

        public async Task<PlayerModel?> GetPlayer(string kingonomyToken)
        {
            return await TryToGetUserFromCache(kingonomyToken).ConfigureAwait(false);
        }

        public async Task<PlayerModel?> Authorize(UnityAuthorizationModel authorizationModel)
        {
            var unityUser = await _authorizationService.ValidateTokenAsync(authorizationModel.AccessToken).ConfigureAwait(false);
            if (unityUser == null) return null;

            var user = await new GetPlayerQuery(unityUser).Execute().ConfigureAwait(false);
            if (user == null)
            {
                // User is not created yet.

            }
            //await _cache.SetRecordAsync(id, result, TimeSpan.MaxValue, TimeSpan.MaxValue);
        }

        private async Task CreateUser(string unityId)
        {
            var query = new AddPlayerItemQuery()
        }
    }
}
