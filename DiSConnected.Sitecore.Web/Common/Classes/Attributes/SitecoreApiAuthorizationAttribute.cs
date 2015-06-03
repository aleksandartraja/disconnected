using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using DiSConnected.Sitecore.Web.Common.Classes.Services;

namespace DiSConnected.Sitecore.Web.Common.Classes.Attributes
{
    [Obsolete("Please use Authorize unless planning to implement custom auth handler")]
    public class SitecoreApiAuthorizationAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            //Code here only for debugging purposes
            string usernameFromSitecoreAuth = IdentityService.GetUsernameFromSitecoreAuth();
            string usernameFromWindowsAuth = IdentityService.GetUsernameFromWindowsAuth();
            return base.IsAuthorized(actionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            string usernameFromSitecoreAuth = IdentityService.GetUsernameFromSitecoreAuth();
            string usernameFromWindowsAuth = IdentityService.GetUsernameFromWindowsAuth();
            base.HandleUnauthorizedRequest(actionContext);
        }
    }
}