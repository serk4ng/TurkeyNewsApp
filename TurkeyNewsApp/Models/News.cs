using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurkeyNewsApp.Models
{
    public class News
    {
 
        public string Author { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public string SourceName { get; set; }
        public sources source { get; set; }

        public class sources
        {
            public string Id { get; set; }
            public string name { get; set; }
        }
    }
}