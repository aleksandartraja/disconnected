using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using DiSConnected.Sitecore.Web.Common.Classes.Util;
using WebApi.OutputCache.V2;

namespace DiSConnected.Sitecore.Web.Controllers
{
    /// <summary>
    /// Sitecontroller: this controller is intended to feed the static site elements, ie: banners, header, footer, menu, notification/message stuff.
    /// Generally I would use the content tree or a global "settings" item to aggregate the data needed to be passed to the front end, currently
    /// there is only just some hard set data, feel free to elaborate...
    /// </summary>
    [RoutePrefix("content_delivery/api/site")]
    [Route("{action=Get}")]
    public class SiteController : ApiController
    {
        // GET api/<controller>
        [CacheOutput(ClientTimeSpan = 300, ServerTimeSpan = 300)]//decided to cache this, very rarely will change.  5 min on both client and server...
        public async Task<object> Get()
        {
            var currentContext = HttpContext.Current;
            return await Task.Run(() => GetAsync(currentContext));
        }

        private object GetAsync(HttpContext currentContext)
        {
            HttpContext.Current = currentContext;
            var footerLinks = new List<object>();
            footerLinks.Add(new { Title = "Terms and Conditions", Url = "terms-and-conditions" });
            footerLinks.Add(new { Title = "FAQ", Url = "faq" });

            var navLinks = new List<object>();
            navLinks.Add(new { Title = "Home", Url = "/" });
            navLinks.Add(new { Title = "Subsection 1", Url = "subsection1/" });
            navLinks.Add(new { Title = "Subsection 2", Url = "subsection2/" });

            var footer = new { Copyright = "DiSConnected Demo Copyright - 2015", FooterLogo = RestUtil.ResolveSitecoreIcon("Imaging/16x16/cut_object.png"), Links = footerLinks };
            var header = new { HeaderLogo = RestUtil.ResolveSitecoreIcon("Imaging/16x16/cut_object.png"), Sitename = "DiSConnected" };
            var navigation = new { Links = navLinks };

            var retVal = new { Navigation = navigation, Header = header, Footer = footer };
            return retVal;
        }
    }
}