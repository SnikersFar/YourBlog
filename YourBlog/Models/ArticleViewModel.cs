using System.ComponentModel.DataAnnotations;

namespace YourBlog.Models
{
    public class ArticleViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Category not correct! (empty)")]
        public long CategoryId { get; set; }
        public long CreatorId { get; set; }
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Title not correct! (empty)")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Text not correct! (empty)")]
        public string Text { get; set; }
        public string Image { get; set; }
        public string Tags { get; set; }
    }
}
