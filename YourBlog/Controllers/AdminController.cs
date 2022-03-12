using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YourBlog.EfStuff.DbModel;
using YourBlog.EfStuff.Repositories;
using YourBlog.Models;
using YourBlog.Services;

namespace YourBlog.Controllers
{
    public class AdminController : Controller
    {
        private UserRepository _userRepository;
        private UserService _userService;

        public AdminController(UserRepository userRepository, UserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel() { ResultLogin = true });
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login, string password)
        {
            var user = _userRepository.GetByNameAndPassword(login, password);
            if (user == null)
            {
                return View(new LoginViewModel() { Login = login, Password = password, ResultLogin = false});
            }

            var claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim("Name", user.Name));
            claims.Add(new Claim(ClaimTypes.AuthenticationMethod, Startup.AuthCoockieName));

            var claimsIdentity = new ClaimsIdentity(claims, Startup.AuthCoockieName);
            var principal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Profile", "Admin");
        }
        [HttpPost]
        public async Task<IActionResult> Register(string login, string password)
        {
            var user = new User() { Name = login, Password = password, IsActive = true };
            _userRepository.Save(user);
            var claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim("Name", user.Name));
            claims.Add(new Claim(ClaimTypes.AuthenticationMethod, Startup.AuthCoockieName));

            var claimsIdentity = new ClaimsIdentity(claims, Startup.AuthCoockieName);
            var principal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Profile", "Admin");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Profile()
        {
            return View();

        }
        public async Task<IActionResult> Logout(LoginViewModel viewModel)
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        [HttpGet]
        public IActionResult ChangeArticle()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public IActionResult ChangeArticle(long idArticle)
        {
            var MeUser = _userService.GetCurrentUser();
            var MyArticle = MeUser.Articles.SingleOrDefault(a => a.Id == idArticle);
            if (MyArticle != null)
            {
                return View(MyArticle);
            } else
            {
                return RedirectToAction("Profile", "Admin");
            }
            
        }
        [Authorize]
        [HttpPost]
        public IActionResult ChangeArticle(ArticleViewModel article)
        {



            return RedirectToAction("Profile", "Admin");
        }
    }

}
