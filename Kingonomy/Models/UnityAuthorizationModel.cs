using System;

namespace Kingonomy.Models
{
    /// <summary>
    /// To retrieve UnityId and Access token look into code example below.
    /// <example>
    /// <code>
    /// await UnityServices.InitializeAsync();
    /// await AuthenticationService.Instance.SignInAnonymouslyAsync(new SignInOptions() { CreateAccount = true });
    /// var playerId = AuthenticationService.Instance.PlayerId;
    /// var accessToken = AuthenticationService.Instance.AccessToken;
    /// </code>
    /// </example>
    /// </summary>
    [Preserve, Serializable]
    public sealed class UnityAuthorizationModel
    {
        [Preserve] public string UnityId { get; set; }
        [Preserve] public string AccessToken { get; set; }
        [Preserve] public bool Create { get; set; }
        [Preserve] public UnityAuthorizationModel(string unityId, string accessToken, bool create)
        {
            UnityId = unityId;
            AccessToken = accessToken;
            Create = create;
        }
    }
}
