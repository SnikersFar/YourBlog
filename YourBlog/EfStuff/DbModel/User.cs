using System.Collections.Generic;

namespace YourBlog.EfStuff.DbModel
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual List<Article> Articles { get; set; }
    }
}