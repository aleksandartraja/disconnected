using System.Web.Mvc;

namespace DiSConnected.Angular.Web.Web.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Unsupported browser error page
        /// </summary>
        /// <returns></returns>
        public ActionResult Unsupported()
        {
            return View();
        }

        /// <summary>
        /// Application error page
        /// </summary>
        /// <returns></returns>
        public ActionResult Application()
        {
            return View();
        }
    }
}