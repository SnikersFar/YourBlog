﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private EfStuff.Repositories.UserRepository _userRepository;
        private Services.UserService _userService;
        private ArticleRepository _articleRepository;
        private CategoryRepository _categoryRepository;
        private ReportService _reportService;
        private IMapper _mapper;

        public AdminController(
            EfStuff.Repositories.UserRepository userRepository,
            Services.UserService userService,
            IMapper mapper,
            CategoryRepository categoryRepository,
            ArticleRepository articleRepository,
            ReportService reportService)
        {
            _userRepository = userRepository;
            _userService = userService;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _articleRepository = articleRepository;
            _reportService = reportService;
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
            if (user == null || !user.IsActive)
            {
                return View(new LoginViewModel() { ResultLogin = true });
            }

            var claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim("Name", user.Name));
            claims.Add(new Claim(ClaimTypes.Role,user.Name));
            claims.Add(new Claim(ClaimTypes.AuthenticationMethod, Startup.AuthCoockieName));

            var claimsIdentity = new ClaimsIdentity(claims, Startup.AuthCoockieName);
            var principal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Profile", "Admin");
        }
        [HttpPost]
        public async Task<IActionResult> Register(string login, string password)
        {
            var userS = _userRepository.GetByNameAndPassword(login, password);
            if (userS != null)
            {
                return RedirectToAction("Login", "Admin");
            }
            var user = new User() { Name = login, Password = password, IsActive = true };
            _userRepository.Save(user);
            var claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim("Name", user.Name));
            claims.Add(new Claim(ClaimTypes.Role, user.Name));
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
            var ViewArticles = _mapper.Map<List<ArticleViewModel>>(_userService.GetCurrentUser().Articles.Where(a => a.IsActive).ToList());
            return View(ViewArticles);

        }
        public async Task<IActionResult> Logout(LoginViewModel viewModel)
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangeArticle(long idArticle = 0)
        {
            var ChangeArticle = new ChangeArticleViewModel()
            {
                Article = new ArticleViewModel(),
                Categories = _mapper.Map<List<CategoryViewModel>>(_categoryRepository.GetAll()),
            };
            if (idArticle == 0)
            {
                return View(ChangeArticle);
            }
            var MeUser = _userService.GetCurrentUser();
            var MyArticle = MeUser.Articles.SingleOrDefault(a => a.Id == idArticle && a.IsActive == true);
            if (MyArticle != null)
            {
                ChangeArticle.Article = _mapper.Map<ArticleViewModel>(MyArticle);
                return View(ChangeArticle);
            }
            else
            {
                return RedirectToAction("Profile", "Admin");
            }

        }
        [Authorize]
        [HttpPost]
        public IActionResult ChangeArticle(ArticleViewModel article)
        {
            if (!ModelState.IsValid)
            {
                var ChangeArticle = new ChangeArticleViewModel()
                {
                    Article = article,
                    Categories = _mapper.Map<List<CategoryViewModel>>(_categoryRepository.GetAll()),
                };
                return View(ChangeArticle);
            }
            var MeUser = _userService.GetCurrentUser();
            if (article.CreatorId == 0 || _articleRepository.Get(article.Id).Creator.Id == MeUser.Id)
            {
                var MyArticle = _mapper.Map<Article>(article);
                MyArticle.Creator = MeUser;
                MyArticle.IsActive = true;
                if(article.CreatorId <= 0)
                {
                    MyArticle.CreatedDate = DateTime.Today;
                }
                MyArticle.IsCategory = _categoryRepository.Get(article.CategoryId);
                _articleRepository.Save(MyArticle);
            }

            return RedirectToAction("Profile", "Admin");
        }

        public IActionResult DeleteArticle(long articleId)
        {
            var MeUser = _userService.GetCurrentUser();
            if (MeUser.Articles.Any(a => a.Id == articleId))
            {
                _articleRepository.Remove(articleId);
            }
            return RedirectToAction("Profile", "Admin");
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddCategory() => View(_mapper.Map<List<CategoryViewModel>>(_categoryRepository.GetAll()));


        [Authorize]
        [HttpPost]
        public IActionResult AddCategory(CategoryViewModel category)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddCategory", "Admin");
            }
            var MyCategory = _categoryRepository.Get(category.Id);
            if (MyCategory != null && MyCategory.IsActive == true)
            {
                MyCategory.Name = category.Name;
                _categoryRepository.Save(MyCategory);

            } else if (category.Id <= 0)
            {
                var newCategory = new Category() { IsActive = true, Name = category.Name, };
                _categoryRepository.Save(newCategory);
            }


            var categories = _mapper.Map<List<CategoryViewModel>>(_categoryRepository.GetAll());
            return View(categories);
        }

        [Authorize]
        [HttpGet]
        public IActionResult DeleteCategory(long Id)
        {
            var category = _categoryRepository.Get(Id);
            if (category != null && category.Articles.Where(a => a.IsActive).ToList().Count == 0)
            {
                category.IsActive = false;
                _categoryRepository.Save(category);
            }
            return RedirectToAction("AddCategory", "Admin");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Reports()
        {
            return View(_reportService.GetAllReports());
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult BanByReport(BanViewModel banView)
        {
            _reportService.Ban(banView);
            return RedirectToAction("Reports");
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddReport(ReportRequestViewModel reportRequest)
        {
            var user = _userService.GetCurrentUser();
            if (user is null)
                return RedirectToAction("InfoArticle", "Home", new { IdArticle = reportRequest.ArticleId});
            reportRequest.ReportAuthorId = user.Id;
            _reportService.AddReport(reportRequest);
            return RedirectToAction("InfoArticle", "Home", new { IdArticle = reportRequest.ArticleId });
        }
    }

}
