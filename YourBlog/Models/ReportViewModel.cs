using YourBlog.EfStuff.DbModel;

namespace YourBlog.Models
{
    public class ReportViewModel
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public virtual ArticleViewModel Post { get; set; }
        public virtual UserViewModel ReportAuthor { get; set; }
    }
}
