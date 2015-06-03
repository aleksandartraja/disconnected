using System;
using System.Collections.Generic;

namespace DiSConnected.Sitecore.Web.Common.Classes.Dtos
{
    public class ArticleDto
    {
        public string Title;
        public Guid Id;
        public string Icon;
        public string Summary;
        public string Author;
        public string Type;
        public string Text;
        public List<string> Tags;
    }
}