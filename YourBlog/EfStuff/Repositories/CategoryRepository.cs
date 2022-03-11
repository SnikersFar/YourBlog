using WebMaze.EfStuff.Repositories;
using YourBlog.EfStuff.DbModel;

namespace YourBlog.EfStuff.Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(WebContext webContext) : base(webContext)
        {
        }
    }
}
