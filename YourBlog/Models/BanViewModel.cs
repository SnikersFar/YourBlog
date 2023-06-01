using System;

namespace YourBlog.Models
{
    public class BanViewModel
    {
        public int ArticleId { get; set; }
        public int UserId { get; set; }
        public TypeBan TypeBan { get; set; }
    }
    [Flags]
    public enum TypeBan
    {
        User = 1,
        Article = 2,
        AllArticles = 4,
    }
}
