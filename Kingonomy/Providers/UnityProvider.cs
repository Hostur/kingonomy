using System.Collections;
using Kingonomy.Models;

namespace Kingonomy.Providers
{
    internal sealed class UnityProvider : UnityHttp
    {
        #region Endpoints
        private const string AUTHORIZE = "api/authorization/unity";
        private const string GET_PLAYER = "api/player";

        #region Resources
        private const string GET_RESOURCES = "api/resources";
        private const string GET_PLAYER_RESOURCES = "api/resources/{0}";
        private const string POST_INCREMENT_PLAYER_RESOURCE = "api/resources/increment/{0}/{1}/{2}";
        private const string POST_DECREMENT_PLAYER_RESOURCE = "api/resources/decrement/{0}/{1}/{2}";
        private const string POST_SET_PLAYER_RESOURCE = "api/resources/set/{0}/{1}/{2}";
        #endregion

        #region Items
        private const string GET_ITEMS = "api/items";
        private const string GET_PLAYER_ITEMS = "api/items/{0}";
        private const string POST_ADD_PLAYER_ITEM = "api/items/add/{0}/{1}";
        private const string DELETE_PLAYER_ITEM = "api/items/delete/{0}/{1}";
        private const string POST_MODIFY_PLAYER_ITEM = "api/items/override";
        #endregion

        #region Development
        private const string POST_ADD_OR_MODIFY_ITEM = "api/items/add_or_modify";
        #endregion

        #endregion

        private string? _authorizationToken;
        private string GetUrl(string url) => string.Format(KingonomySettings.BaseUrl, KingonomySettings.Environment, url);
        public bool Initialized => !string.IsNullOrEmpty(_authorizationToken);

        /// <summary>
        /// Init unity provider for Kingonomy.
        /// </summary>
        /// <param name="http">Unity http request representation returning value indicating whether we initialized it or not.</param>
        /// <param name="unityAuthorizationModel">Unity authorization model.</param>
        public IEnumerator Init(IUnityHttp<string> http, UnityAuthorizationModel unityAuthorizationModel)
        {
            yield return Post(GetUrl(AUTHORIZE), unityAuthorizationModel, http, true);

            if (http.IsDone && http.Success)
            {
                _authorizationToken = http.Result;
            }
        }
        
        public IEnumerator GetPlayer(IUnityHttp<PlayerModel> http)
        {
            yield return Get(GetUrl(GET_PLAYER), _authorizationToken, http);
        }

        #region Items

        public IEnumerator GetItems(IUnityHttp<PlayerItemModel[]> http)
        {
            yield return Get(GetUrl(GET_ITEMS), _authorizationToken, http);
        }

        /// <summary>
        /// Require extended permissions [KingonomySettings.AuthorizationToken].
        /// </summary>
        public IEnumerator GetItems(IUnityHttp<PlayerItemModel[]> http, string playerUnityId)
        {
            yield return Get(GetUrl(string.Format(GET_PLAYER_ITEMS, playerUnityId)), KingonomySettings.AuthorizationToken, http);
        }

        public IEnumerator AddPlayerItem(IUnityHttp<bool> http, string playerUnityId, string itemId)
        {
            yield return Post(GetUrl(string.Format(POST_ADD_PLAYER_ITEM, playerUnityId, itemId)), http);
        }

        public IEnumerator RemovePlayerId(IUnityHttp<bool> http, string playerUnityId, string itemId)
        {
            yield return Delete(GetUrl(string.Format(DELETE_PLAYER_ITEM, playerUnityId, itemId)), http);
        }

        public IEnumerator ModifyPlayerItem(IUnityHttp<bool> http, PlayerItemModel playerItemModel)
        {
            yield return Post(GetUrl(POST_MODIFY_PLAYER_ITEM), playerItemModel, http);
        }
        #endregion

        #region Development

        public IEnumerator AddOrModifyItem(IUnityHttp<bool> http, ItemModel itemModel)
        {
            yield return Post(GetUrl(POST_ADD_OR_MODIFY_ITEM), itemModel, http);
        }

        #endregion
    }
}
