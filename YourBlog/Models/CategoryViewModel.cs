using System.ComponentModel.DataAnnotations;

namespace YourBlog.Models
{
    public class CategoryViewModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public long CountArticles { get; set; }
    }
}