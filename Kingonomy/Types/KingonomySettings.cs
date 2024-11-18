using System;

namespace Kingonomy
{
    [Preserve] public enum Environment { Staging, Production }

    [Preserve, Serializable]
    public class KingonomySettings
    {
        /// <summary>
        /// If your client have extended permissions.
        /// </summary>
        public static string? AuthorizationToken;

        /// <summary>
        /// Use for staging, production etc.
        /// </summary>
        public static Environment Environment;

        public static float Timeout = 8f;
        public static string BaseUrl => "https://kingonomy.kinguin.net/{0}/{1}";
    }
}
