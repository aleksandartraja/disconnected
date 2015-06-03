using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;

namespace DiSConnected.Sitecore.Web
{
    public class Application : global::Sitecore.Web.Application
    {
        protected void Application_Error(object sender, EventArgs e)
        {
            // if not configured to show friendly errors or Custom Errors to remote only and we are currently Local
            // do not handle any exceptions.
            if (global::Sitecore.Configuration.Settings.CustomErrorsMode == CustomErrorsMode.Off || (global::Sitecore.Configuration.Settings.CustomErrorsMode == CustomErrorsMode.RemoteOnly && HttpContext.Current.Request.IsLocal))
            {
                return;
            }

            // get and log the exception.
            // Sitecore will also try to log the exception, 
            // which can lead to some duplicate messages in the logs,
            // but this message includes the requested URL,
            // which could be useful for diagnosing issues.
            Exception exception = Server.GetLastError();
            global::Sitecore.Diagnostics.Log.Error(this + " : Exception processing " + global::Sitecore.Context.RawUrl, exception, this);

            // we're going to handle this exception.
            Response.Clear();
            Server.ClearError();

            // treat all exceptions as HTTP 500 by default
            // and redirect/tranfer to a generic error page
            string url = global::Sitecore.Configuration.Settings.ErrorPage;

            // query string parameters to add to the URL
            List<string> list = new List<string>();

            // check if it's an HTTP 404.
            // if it is, change the redirect/transfer URL;
            // otherwise, add the error query string parameter
            // used by the generic error page.
            HttpException httpException = exception as HttpException;

            if (httpException != null && httpException.GetHttpCode() == 404)
            {
                url = global::Sitecore.Configuration.Settings.ItemNotFoundUrl;
            }
            else
            {
                list.Add("error");
                list.Add(global::Sitecore.Globalization.Translate.Text("An unhandled exception occurred."));
            }

            // the ExecuteRequest pipeline processor adds these for 404 conditions,
            // and it doesn't hurt to add them for any other conditons.
            list.Add("user");
            list.Add(global::Sitecore.Context.User != null ? global::Sitecore.Context.User.Name : String.Empty);
            list.Add("site");
            list.Add(global::Sitecore.Context.Site != null ? global::Sitecore.Context.Site.Name : String.Empty);

            // if configured to add the URL to the query string on errors, add it.
            if (global::Sitecore.Configuration.Settings.Authentication.SaveRawUrl)
            {
                list.Add("url");
                list.Add(global::Sitecore.Context.RawUrl != null ? global::Sitecore.Context.RawUrl : String.Empty);
            }

            // add the query string parameters (this encodes them automatically).
            url = global::Sitecore.Web.WebUtil.AddQueryString(url, list.ToArray());

            // if configured to transfer, transfer, otherwise redirect.
            if (global::Sitecore.Configuration.Settings.RequestErrors.UseServerSideRedirect)
            {
                HttpContext.Current.Server.Transfer(url);
            }
            else
            {
                global::Sitecore.Web.WebUtil.Redirect(url, false);
            }
        }
    }
}
