using System.Collections.Generic;

namespace YourBlog.EfStuff.DbModel
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public virtual List<Article> Articles { get; set; }
    }
}