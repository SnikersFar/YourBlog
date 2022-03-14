using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YourBlog.Models
{
    public class ChangeArticleViewModel
    {
        public List<CategoryViewModel> Categories { get; set; }
        public ArticleViewModel Article { get; set; }
    }
}
