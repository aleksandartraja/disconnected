using System;
using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;
using DiSConnected.Sitecore.Web.App_Start;
using DiSConnected.Sitecore.Web.Common.Classes.Caching;
using DiSConnected.Sitecore.Web.Common.Classes.Interfaces;
using Newtonsoft.Json.Serialization;
using WebApi.OutputCache.V2;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(WebApiConfig), "Start")]

namespace DiSConnected.Sitecore.Web.App_Start
{
    public class WebApiConfig
    {
        public static void Start()
        {
            GlobalConfiguration.Configure(Register);
            SetupMappingsItemClassToItemClassDTO();
        }

        public static EndpointCache EndpointCache = new EndpointCache();

        public static void Register(HttpConfiguration config)
        {
            // initialize and map all attribute routed Web API controllers (note: this does not enable MVC attribute routing)
            var cors = new EnableCorsAttribute("*", "*", "*") { SupportsCredentials = true };//TODO: Limit domains to acceptable domains
            config.EnableCors(cors);
            config.MapHttpAttributeRoutes();

            RegisterCacheProvider(config);


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "content_delivery/api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // force JSON responses only (no XML)
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

#if (DEBUG)
            jsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
#endif
        }

        private static void RegisterCacheProvider(HttpConfiguration config)
        {
            var disableCacheConfigString = ConfigurationManager.AppSettings["DisableServiceOutputCache"];
            bool disable = false;
            bool temp;

            if (bool.TryParse(disableCacheConfigString, out temp))
            {
                disable = temp;
            }

            if (disable)
            {
                config.CacheOutputConfiguration().RegisterCacheOutputProvider(() => new NullOutputCache());
            }
            else
            {
                config.CacheOutputConfiguration().RegisterCacheOutputProvider(() => EndpointCache);
            }

        }

        public static void SetupMappingsItemClassToItemClassDTO()
        {
            var baseAssembly = Assembly.Load("DiSConnected.Sitecore.Web");

            var baseAssemblyDtoTypes = baseAssembly.GetTypes()
               .Where(t => t.GetInterfaces().Contains(typeof(IDtoMapping)));
            
            foreach (var dtoMappingType in baseAssemblyDtoTypes)
            {
                var currentType = Activator.CreateInstance(dtoMappingType);
                var createMappingsMethod = dtoMappingType.GetMethod("CreateMappings");
                createMappingsMethod.Invoke(currentType, null);
            }
        }
    }
}