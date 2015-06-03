using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using Sitecore.StringExtensions;
using Sitecore.Web.UI.WebControls;
using DiSConnected.Sitecore.Web.Common.Classes.Services;

namespace DiSConnected.Sitecore.Web.Common.Classes.Util
{
    public static class RestUtil
    {
        /// <summary>
        /// A simple string format and replace to make a machine/code like name
        /// </summary>
        /// <param name="currentItem"></param>
        /// <returns></returns>
        public static string GetComponentCodeName(Item currentItem)
        {
            string retVal = GetComponentCodeName(currentItem.TemplateName);
            return retVal;
        }

        /// <summary>
        /// A simple string format and replace to make a machine/code like name
        /// </summary>
        /// <param name="currentItemTemplateName"></param>
        /// <returns></returns>
        public static string GetComponentCodeName(string currentItemTemplateName)
        {
            string retVal = "";
            if (!string.IsNullOrEmpty(currentItemTemplateName))
            {
                retVal = currentItemTemplateName.ToLower().Replace(" ", "_");
            }
            return retVal;
        }

        /// <summary>
        /// Simply get the ID from the component
        /// </summary>
        /// <param name="currentItem"></param>
        /// <returns></returns>
        public static string GetComponentId(Item currentItem)
        {
            string retVal = currentItem.ID.ToString();
            return retVal;
        }

        /// <summary>
        /// Runs a field thru the sitecore fieldrender and disabls web-editing, thus getting a field value as if it ran thru the render pipeline
        /// </summary>
        /// <param name="currentItem"></param>
        /// <param name="currentFieldName"></param>
        /// <returns></returns>
        public static string ProcessSitecoreField(Item currentItem, string currentFieldName)
        {
            var retVal = "";
            try
            {
                retVal = HttpUtility.HtmlDecode(FieldRenderer.Render(currentItem, currentFieldName, "disable-web-editing=true"));//disable web edit entirely
            }
            catch (Exception)
            {
                throw new Exception(string.Format("RestUtil.ProcessSitecoreField - Error Processing: {0}:{1} - {2}", currentItem.DisplayName, currentItem.ID.ToString(), currentFieldName));
            }
            return retVal;
        }

        /// <summary>
        /// Gets related url for the Media item
        /// </summary>
        /// <param name="currentMediaItem"></param>
        /// <param name="sourceItem"></param>
        /// <param name="thumbnail"></param>
        /// <returns></returns>
        public static string GetMediaUrl(Item currentMediaItem, Item sourceItem = null, bool thumbnail = false)
        {
            var retVal = "";
            try
            {
                if (currentMediaItem != null)
                {
                    var mediaUrlOptions = new MediaUrlOptions();
                    //Feel free to handle different images as they need ie:
                    //mediaUrlOptions.MaxWidth = 750;
                    //mediaUrlOptions.MaxHeight = 500;

                    retVal = MediaManager.GetMediaUrl(currentMediaItem, mediaUrlOptions);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return retVal;
        }

        /// <summary>
        /// Create links for a specific item that are fully deep linkable from the FE/angular.  Uses the template name to determine the route.
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public static string DeepLinkHandler(string itemId)
        {
            var currentItem = Context.Database.GetItem(new ID(itemId));
            return DeepLinkHandler(currentItem, null);
        }

        /// <summary>
        /// Create links for a specific item that are fully deep linkable from the FE/angular.  Uses the template name to determine the route.
        /// </summary>
        /// <param name="currentItem"></param>
        /// <returns></returns>
        public static string DeepLinkHandler(Item currentItem)
        {
            return DeepLinkHandler(currentItem, null);
        }

        /// <summary>
        /// Create links for a specific item that are fully deep linkable from the FE/angular.  Uses the template name to determine the route.
        /// </summary>
        /// <param name="currentItem"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public static string DeepLinkHandler(Item currentItem, string currentUser)
        {
            var retVal = "";

           //Handle deep link relevant to the front end/angular
            //For example, if using CIG and given our current SampleItem type of template...

            //if (currentItem.IsOfTemplate(SampleItem.TemplateId.ToString(), true))
            //    retVal = string.Format("/article/{0}", currentItem.ID);
            //else if (currentItem.IsOfTemplate(SampleItem2.TemplateId.ToString(), true) || currentItem.IsOfTemplate(TeaserItem.TemplateId.ToString(), true))
             //   retVal = string.Format("/teaser/{0}", currentItem.ID);

            //this would be a very programatic way to handle any instance of templates you generate, but for now lets just use this:

            retVal = string.Format("{0}/{1}", GetComponentCodeName(currentItem), currentItem.ID.ToGuid());

            return retVal;
        }

        /// <summary>
        /// Truncate function to restrict length and add ellipsis
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxChars"></param>
        /// <returns></returns>
        public static string Truncate(string value, int maxChars)
        {
            string retVal = "";
            if (!value.IsNullOrEmpty())
                retVal = (value.Length <= maxChars ? value : value.Substring(0, maxChars) + " ...");
            return retVal;
        }

        /// <summary>
        /// Gets user Id, if there is none, or it fails returns null
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUserAccountName()
        {
            string retVal = null;
            try
            {
                if (HttpContext.Current != null)
                {
                    var profile = IdentityService.GetUsernameFromSitecoreAuth();
                    retVal = profile;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to obtain Current User (RestUtil)", exception); //swallow the exception, its a Util
            }
            return retVal;
        }

        /// <summary>
        /// Used to parse out (via regex) http(s) and www links
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<string> ParseForLinks(string input)
        {
            Regex linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            List<string> listOfLinks = new List<string>();
            foreach (Match m in linkParser.Matches(input))
            {
                string currentMatch = "";
                //if (!m.Value.StartsWith("http://") && !m.Value.StartsWith("https://"))
                //    currentMatch = "http://" + m.Value;
                //else
                currentMatch = m.Value;
                listOfLinks.Add(currentMatch);
            }
            return listOfLinks;
        }

        public static string HtmlEncode(string input)
        {
            return HttpUtility.HtmlEncode(input);
        }

        /// <summary>
        /// Used to provide a static reference back to the hostname on async requests
        /// </summary>
        public static string CurrentHostname { get; set; }

        /// <summary>
        /// See if a string contains a given string, with comparison for case sensitivity
        /// </summary>
        /// <param name="source"></param>
        /// <param name="toCheck"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        /// <summary>
        /// Method to getting the full path to a sitecore provided icon
        /// </summary>
        /// <param name="iconShortPath"></param>
        /// <returns></returns>
        public static string ResolveSitecoreIcon(string iconShortPath)
        {
            return string.Format("{2}://{0}/~/icon/{1}", HttpContext.Current.Request.Url.Host, iconShortPath, HttpContext.Current.Request.Url.Scheme);
        }

    }
}
