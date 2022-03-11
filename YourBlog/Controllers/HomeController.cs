using Microsoft.AspNetCore.Mvc;

namespace YourBlog.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
