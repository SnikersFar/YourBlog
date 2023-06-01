namespace YourBlog.Models
{
    public class ReportRequestViewModel
    {
        public string ReportMessage { get; set; }
        public long ArticleId { get; set; }  
        public long ReportAuthorId { get; set; }  
    }
}
