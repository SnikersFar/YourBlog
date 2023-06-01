using System.Collections.Generic;

namespace YourBlog.EfStuff.DbModel
{
    public class Report : BaseModel
    {
        public string Message { get; set; }
        public virtual Article Post { get; set; }
        public virtual User ReportAuthor { get; set; }
    }
}
