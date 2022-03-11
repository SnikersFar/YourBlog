using WebMaze.EfStuff.Repositories;
using YourBlog.EfStuff.DbModel;

namespace YourBlog.EfStuff.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(WebContext webContext) : base(webContext)
        {
        }
    }
}
