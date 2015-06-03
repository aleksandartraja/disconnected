using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using SitecoreContext = Sitecore.Context;
using AutoMapper;
using DiSConnected.Sitecore.Web.Common.Classes.Dtos;

namespace DiSConnected.Sitecore.Web.Controllers
{
    /// <summary>
    /// The intention of this controller is to serve up specific content to the front end.  It is currently named "article" controller only to demonstrate the 
    /// seperation of controllers based on content type (article, blog, document, etc.)  
    /// This would be a good time to use CustomItemGenerator (https://marketplace.sitecore.net/en/Modules/Custom_Item_Generator.aspx)
    /// to seperate these controllers further to specific generated classes.  But for the sake of example, I will merely use automapper to pull a generic item...
    /// </summary>
    [RoutePrefix("content_delivery/api/article")]
    [Route("{action=Get}")]
    public class ArticlesController : ApiController
    {
        // GET api/<controller>
        [Route("")]
        public async Task<object> Get()
        {
            var currentContext = HttpContext.Current;
            return await Task.Run(() => GetAsync(currentContext));
        }

        private object GetAsync(HttpContext currentContext)
        {
            HttpContext.Current = currentContext;
            var retVal = new List<ArticleDto>();
            using (new DatabaseSwitcher(Factory.GetDatabase("web")))
            {
                var contentRoot = SitecoreContext.Site.ContentStartPath;//would allow for multi-site handling...
                var templateName = "Sample Item";//CIG - TemplateItem.TemplateID, and modify select query to handle template id
                var selectQuery = string.Format("{0}//*[@@templatename = \"{1}\"]", contentRoot, templateName);
                var selectedItems = SitecoreContext.Data.Database.SelectItems(selectQuery);
                retVal = Mapper.Map<List<Item>, List<ArticleDto>>(selectedItems.ToList());
            }
            return retVal;
        }

        // GET api/<controller>/5
        [Route("")]
        [Route("{id}")]
        public async Task<object> Get(Guid id)
        {
            var currentContext = HttpContext.Current;
            return await Task.Run(() => GetByIdAsync(currentContext, id));
        }

        private object GetByIdAsync(HttpContext currentContext, Guid id)
        {
            HttpContext.Current = currentContext;
            var retVal = new ArticleDto();
            using (new DatabaseSwitcher(Factory.GetDatabase("web")))
            {
                var selectedItem = SitecoreContext.Data.Database.GetItem(new ID(id));
                retVal = Mapper.Map<Item, ArticleDto>(selectedItem);
            }
            return retVal;
        }
    }
}