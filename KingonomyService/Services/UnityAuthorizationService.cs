using System.Net.Http.Headers;

namespace KingonomyService.Services
{
    public sealed class UnityAuthorizationService
    {
        private const string? UNITY_VALIDATION_ENDPOINT = "https://api.unity.com/v1/identity";

        public async Task<string?> ValidateTokenAsync(string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                try
                {
                    var response = await httpClient.GetAsync(UNITY_VALIDATION_ENDPOINT);

                    if (response.IsSuccessStatusCode)
                    {
                        var userData = await response.Content.ReadAsStringAsync();
                        return userData;
                    }
                    else
                    {
                        return null;
                        throw new Exception($"Token validation failed with status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return null;
                    throw new Exception($"Error validating token: {ex.Message}");
                }
            }
        }
    }
}
