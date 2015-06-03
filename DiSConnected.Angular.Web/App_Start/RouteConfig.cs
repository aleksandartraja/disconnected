using System.Web.Mvc;
using System.Web.Routing;

namespace DiSConnected.Angular.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Poke a hole through the routing for static files
            routes.RouteExistingFiles = false;

            // Unsupported browser error route
            routes.MapRoute(
                "Error-Unsupported",
                "Error/Unsupported",
                new { Controller = "Error", action = "Unsupported", id = "" });

            // Application error route
            routes.MapRoute(
                "Error-Application",
                "Error/Application",
                new { Controller = "Error", action = "Application", id = "" });

            // Route all other requests to Angular UI Router
            routes.MapRoute(
                "App",
                "{*.}",
                new { controller = "Home", action = "Index" });
        }
    }
}
