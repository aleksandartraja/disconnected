using System.Web.Mvc;
using DiSConnected.Angular.Web.Web.Classes;

namespace DiSConnected.Angular.Web.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //TODO: Add/remove endpoint config references as  you add and remove rest endpoints
            ViewBag.LogLevel = Configuration.LogLevel;
            ViewBag.ArticleEndpoint = Configuration.ArticleEndpoint;
            ViewBag.SiteEndpoint = Configuration.SiteEndpoint;
            return View();
        }
    }
}