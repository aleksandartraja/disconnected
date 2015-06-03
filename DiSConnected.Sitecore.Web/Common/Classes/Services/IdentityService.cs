using System.Web;
using SitecoreContext = Sitecore.Context;

namespace DiSConnected.Sitecore.Web.Common.Classes.Services
{
    public static class IdentityService
    {
        public static string GetUsernameFromWindowsAuth()
        {
            var windowsIdentity = HttpContext.Current.User.Identity.Name;
            return windowsIdentity;
        }

        public static string GetUsernameFromSitecoreAuth()
        {
            var sitecoreIdentity = SitecoreContext.User.Identity.Name;
            return sitecoreIdentity;
        }
    }
}