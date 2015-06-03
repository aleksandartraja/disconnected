﻿﻿using System.Text;
using System.Web.Optimization;

namespace DiSConnected.Angular.Web.Web.Classes
{
    /// <summary>
    /// Transform Angular templates into a single bundle
    /// </summary>
    public class TemplateTransform : IBundleTransform
    {
        private readonly string _moduleName;
        public TemplateTransform(string moduleName)
        {
            _moduleName = moduleName;
        }

        public void Process(BundleContext context, BundleResponse response)
        {
            var strBundleResponse = new StringBuilder();
            // Javascript module for Angular that uses templateCache 
            strBundleResponse.AppendFormat(
                @"angular.module('{0}').run(['$templateCache',function(t){{",
                _moduleName);

            foreach (var file in response.Files)
            {
                // Get the partial page, remove line feeds and escape quotes
                var content = file.ApplyTransforms()
                    .Replace("\r\n", "").Replace("'", "\\'");
                // Create insert statement with template
                strBundleResponse.AppendFormat(
                    @"t.put('{0}','{1}');", file.IncludedVirtualPath.Replace("~", ""), content);
            }
            strBundleResponse.Append(@"}]);");

            response.Files = new BundleFile[] { };
            response.Content = strBundleResponse.ToString();
            response.ContentType = "text/javascript";
        }
    }
}