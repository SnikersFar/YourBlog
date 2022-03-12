namespace YourBlog.Models
{
    public class ArticleViewModel
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }
        public long CreatorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string Tags { get; set; }
    }
}
