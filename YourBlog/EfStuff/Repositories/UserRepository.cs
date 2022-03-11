using System.Linq;
using WebMaze.EfStuff.Repositories;
using YourBlog.EfStuff.DbModel;

namespace YourBlog.EfStuff.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(WebContext webContext) : base(webContext)
        {
        }
        public User GetByNameAndPassword(string login, string password)
        {
            return GetAllQueryable().SingleOrDefault(x => x.Name == login && x.Password == password);
        }
    }
}
