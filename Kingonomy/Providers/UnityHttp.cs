using System;
using System.Collections;

namespace Kingonomy.Providers
{
    /// <summary>
    /// Abstraction that have to be implemented on the Unity side. Every request
    /// is hold by such object to handle corutines.
    /// </summary>
    /// <typeparam name="T">Result expected from this request.</typeparam>
    public interface IUnityHttp<T>
    {
        bool IsDone { get; }
        float Time { get; }
        void Get(string url, string authorizationToken);

        /// <summary>
        /// TA represents post argument that we are sending.
        /// </summary>
        void Post<TA>(string url, TA arg, string authorizationToken);

        /// <summary>
        /// Post body-less.
        /// </summary>
        void Post(string url, string authorizationToken);

        /// <summary>
        /// TA represents post argument that we are sending.
        /// </summary>
        void Put<TA>(string url, TA arg, string authorizationToken);

        void Delete(string url, string authorizationToken);

        void Abort();
        T Result { get; }
        bool Success { get; }
        string ResponseCode { get; }
        string Error { get; }
        Action<T> OnSuccess { get; }
        Action<string> OnError { get; }
    }

    /// <summary>
    /// Hold basic part of the provider logic.
    /// </summary>
    internal abstract class UnityHttp
    {
        #region Get Post Put

        protected IEnumerator Get<T>(string url, string? token, IUnityHttp<T> http)
        {
            if (string.IsNullOrEmpty(token))
            {
                http.OnError?.Invoke("Provided not authorized. Authorization token not set. Call Initialize first.");
                yield break;
            }

            http.Get(url, token);

            float elapsedTime = 0f;
            while (!http.IsDone)
            {
                if (elapsedTime >= KingonomySettings.Timeout)
                {
                    http.Abort();
                    http.OnError?.Invoke($"Request timed out after {elapsedTime}. Url: {url}");
                    yield break;
                }

                elapsedTime += http.Time;
                yield return null;
            }

            if (http.Success)
            {
                var obj = http.Result;
                http.OnSuccess?.Invoke(obj);
            }
            else
            {
                http.OnError?.Invoke($"{http.ResponseCode} - {http.Error}");
            }
        }

        protected IEnumerator Post<T, TA>(string url, TA arg, IUnityHttp<T> http, bool ignoreAuthorizationToken = false)
        {
            if (!ignoreAuthorizationToken && string.IsNullOrEmpty(KingonomySettings.AuthorizationToken))
            {
                http.OnError?.Invoke("KingdomSettings.AuthorizationToken not set. It's required to perform POST action.");
                yield break;
            }

            http.Post(url, arg, KingonomySettings.AuthorizationToken!);

            float elapsedTime = 0f;
            while (!http.IsDone)
            {
                if (elapsedTime >= KingonomySettings.Timeout)
                {
                    http.Abort();
                    http.OnError?.Invoke($"Request timed out after {elapsedTime}. Url: {url}");
                    yield break;
                }

                elapsedTime += http.Time;
                yield return null;
            }

            if (http.Success)
            {
                var obj = http.Result;
                http.OnSuccess?.Invoke(obj);
            }
            else
            {
                http.OnError?.Invoke($"{http.ResponseCode} - {http.Error}");
            }
        }

        protected IEnumerator Post<T>(string url, IUnityHttp<T> http)
        {
            if (string.IsNullOrEmpty(KingonomySettings.AuthorizationToken))
            {
                http.OnError?.Invoke("KingdomSettings.AuthorizationToken not set. It's required to perform POST action.");
                yield break;
            }

            http.Post(url, KingonomySettings.AuthorizationToken!);

            float elapsedTime = 0f;
            while (!http.IsDone)
            {
                if (elapsedTime >= KingonomySettings.Timeout)
                {
                    http.Abort();
                    http.OnError?.Invoke($"Request timed out after {elapsedTime}. Url: {url}");
                    yield break;
                }

                elapsedTime += http.Time;
                yield return null;
            }

            if (http.Success)
            {
                var obj = http.Result;
                http.OnSuccess?.Invoke(obj);
            }
            else
            {
                http.OnError?.Invoke($"{http.ResponseCode} - {http.Error}");
            }
        }

        protected IEnumerator Put<TA>(string url, TA arg, IUnityHttp<bool> http)
        {
            if (string.IsNullOrEmpty(KingonomySettings.AuthorizationToken))
            {
                http.OnError?.Invoke("KingdomSettings.AuthorizationToken not set. It's required to perform PUT action.");
                yield break;
            }

            http.Put(url, arg, KingonomySettings.AuthorizationToken);

            float elapsedTime = 0f;
            while (!http.IsDone)
            {
                if (elapsedTime >= KingonomySettings.Timeout)
                {
                    http.Abort();
                    http.OnError?.Invoke($"Request timed out after {elapsedTime}. Url: {url}");
                    yield break;
                }

                elapsedTime += http.Time;
                yield return null;
            }

            if (http.Success)
            {
                var obj = http.Result;
                http.OnSuccess?.Invoke(obj);
            }
            else
            {
                http.OnError?.Invoke($"{http.ResponseCode} - {http.Error}");
            }
        }

        protected IEnumerator Delete(string url, IUnityHttp<bool> http)
        {
            if (string.IsNullOrEmpty(KingonomySettings.AuthorizationToken))
            {
                http.OnError?.Invoke("KingdomSettings.AuthorizationToken not set. It's required to perform PUT action.");
                yield break;
            }

            http.Delete(url, KingonomySettings.AuthorizationToken);

            float elapsedTime = 0f;
            while (!http.IsDone)
            {
                if (elapsedTime >= KingonomySettings.Timeout)
                {
                    http.Abort();
                    http.OnError?.Invoke($"Request timed out after {elapsedTime}. Url: {url}");
                    yield break;
                }

                elapsedTime += http.Time;
                yield return null;
            }

            if (http.Success)
            {
                var obj = http.Result;
                http.OnSuccess?.Invoke(obj);
            }
            else
            {
                http.OnError?.Invoke($"{http.ResponseCode} - {http.Error}");
            }
        }

        #endregion
    }
}
