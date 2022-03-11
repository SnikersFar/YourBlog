namespace YourBlog.Models
{
    public class ArticleViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public virtual CategoryViewModel IsCategory { get; set; }
        public UserViewModel Creator { get; set; }
        public string Tags { get; set; }
    }
}
