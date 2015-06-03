using System.Web.Optimization;

namespace DiSConnected.Angular.Web.Web.Classes
{
    /// <summary>
    /// Defines a bundle of Angular templates
    /// </summary>
    public class TemplateBundle : Bundle
    {
        public TemplateBundle(string moduleName, string virtualPath)
            : base(virtualPath, new[] { new TemplateTransform(moduleName) })
        {
        }
    }
}