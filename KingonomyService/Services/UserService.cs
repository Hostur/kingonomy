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

        public async Task<PlayerModel?> GetPlayer(string accessToken) => await _cache.GetRecordAsync<PlayerModel>(accessToken);

        public async Task<PlayerModel?> Authorize(UnityAuthorizationModel authorizationModel)
        {
            // Check user in Unity Services
            var unityUser = await _authorizationService.ValidateTokenAsync(authorizationModel.AccessToken).ConfigureAwait(false);
            if (unityUser == null) return null;

            var user = await new GetPlayerQuery(unityUser).Execute().ConfigureAwait(false);
            if (user == null) // If there is no player in our database for this unity user we want to create a new one.
            {
                var newUser = await _dbProvider.AddPlayer(authorizationModel.UnityId, Role.Player, string.Empty).ConfigureAwait(false);
                if (newUser == null)
                {
                    throw new InvalidOperationException($"Failed to create a new player for {authorizationModel.UnityId} : {authorizationModel.AccessToken}");
                }

                // Set player into cache. Player have to exist in this cache to be accepted by a server.
                await _cache.SetRecordAsync(authorizationModel.AccessToken, newUser, TimeSpan.FromDays(1), TimeSpan.FromDays(1));
                return newUser;
            }
            else
            {
                await _cache.SetRecordAsync(authorizationModel.AccessToken, user, TimeSpan.FromDays(1), TimeSpan.FromDays(1));
                return user;
            }
        }

        public async Task RefreshPlayerItems(string accessToken, PlayerItemModel[] items)
        {
            var player = await GetPlayer(accessToken).ConfigureAwait(false);
            if (player == null) return;

            player.Items = items;
            await _cache.SetRecordAsync(accessToken, player, TimeSpan.FromDays(1), TimeSpan.FromDays(1))
                .ConfigureAwait(false);
        }
    }
}
