using System.Web.Configuration;

namespace DiSConnected.Angular.Web.Web.Classes
{
    /// <summary>
    /// Configuration helper class
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// The front end log level
        /// </summary>
        public static string LogLevel
        {
            get { return _logLevel ?? (_logLevel = WebConfigurationManager.AppSettings["LogLevel"]); }
        }
        private static string _logLevel = null;
        
        /// <summary>
        /// Enable bundling defined in /AppStart/BundleConfig.cs
        /// </summary>
        public static bool EnableBundling
        {
            get { return _enableBundling.HasValue ? _enableBundling.Value : (_enableBundling = bool.Parse(WebConfigurationManager.AppSettings["EnableBundling"])).Value; }
        }
        private static bool? _enableBundling = null;

        /// <summary>
        /// The associated Rest Endpoint for articles
        /// </summary>
        public static string ArticleEndpoint
        {
            get { return _articleEndpoint ?? (_articleEndpoint = WebConfigurationManager.AppSettings["ArticleEndpoint"]); }
        }
        private static string _articleEndpoint = null;

        /// <summary>
        /// The associated Rest Endpoint for sitewide content
        /// </summary>
        public static string SiteEndpoint
        {
            get { return _siteEndpoint ?? (_siteEndpoint = WebConfigurationManager.AppSettings["SiteEndpoint"]); }
        }
        private static string _siteEndpoint = null;
    }
}