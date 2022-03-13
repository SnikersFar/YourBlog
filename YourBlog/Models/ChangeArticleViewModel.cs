using System.Collections.Generic;

namespace YourBlog.Models
{
    public class ChangeArticleViewModel
    {
        public List<CategoryViewModel> Categories { get; set; }
        public ArticleViewModel Article { get; set; }
    }
}
