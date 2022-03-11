using WebMaze.EfStuff.Repositories;
using YourBlog.EfStuff.DbModel;

namespace YourBlog.EfStuff.Repositories
{
    public class ArticleRepository : BaseRepository<Article>
    {
        public ArticleRepository(WebContext webContext) : base(webContext)
        {
        }
    }
}
