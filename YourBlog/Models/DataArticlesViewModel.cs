using System;
using System.Collections.Generic;

namespace YourBlog.Models
{
    public class DataArticlesViewModel
    {
        public List<ArticleViewModel> Articles { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public string TagFind { get; set; }
        public long CategoryIdFind { get; set; }
        public DateTime FromDateFind { get; set; }
        public DateTime ToDateFind { get; set; }
        public long PerPage { get; set; }
        public long MyPage { get; set; }
        public int CountPages { get; set; }
    }
}
