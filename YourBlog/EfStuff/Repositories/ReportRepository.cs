using WebMaze.EfStuff.Repositories;
using YourBlog.EfStuff.DbModel;

namespace YourBlog.EfStuff.Repositories
{
    public class ReportRepository : BaseRepository<Report>
    {
        public ReportRepository(WebContext webContext) : base(webContext)
        {
        }
    }
}
