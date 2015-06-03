using System.Web.Optimization;
using DiSConnected.Angular.Web.Web.Classes;

namespace DiSConnected.Angular.Web.Web
{
    public class BundleConfig
    {
        // TODO: For any additionally created directives or templates, please add the necessary lines here...
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // script bundle
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-animate.js",
                "~/Scripts/angular-resource.js",
                "~/Scripts/angular-sanitize.js",
                "~/Scripts/angular-ui-router.js",
                "~/Scripts/angular-touch.js"));

            // app bundle
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/App/configuration/routes.js",
                "~/App/directives/example.js",
                "~/App/directives/page-header.js",
                "~/App/directives/page-navigation.js",
                "~/App/directives/page-footer.js",
                "~/App/factories/article.js",
                "~/App/factories/site.js",
                "~/App/templates/page.js",
                "~/App/templates/home.js",
                "~/App/templates/article.js",
                "~/App/templates/subsection1/subsection1-page.js",
                "~/App/templates/subsection2/subsection2-page.js"));

            // template bundle
            bundles.Add(new TemplateBundle("app", "~/bundles/templates").Include(
                "~/App/directives/example.html",
                "~/App/directives/page-header.html",
                "~/App/directives/page-navigation.html",
                "~/App/directives/page-footer.html",
                "~/App/templates/page.html",
                "~/App/templates/home.html",
                "~/App/templates/article.html",
                "~/App/templates/subsection1/subsection1.html",
                "~/App/templates/subsection1/subsection1-page.html",
                "~/App/templates/subsection2/subsection2.html",
                "~/App/templates/subsection2/subsection2-page.html"));

            // style bundle
            bundles.Add(new StyleBundle("~/Content/style").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));

            BundleTable.EnableOptimizations = Configuration.EnableBundling;
        }
    }
}
