using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using YourBlog.EfStuff.Repositories;
using YourBlog.Models;

namespace YourBlog.Controllers
{
    public class HomeController : Controller
    {
        ArticleRepository _articleRepository;
        CategoryRepository _categoryRepository;
        IMapper _mapper;
        public HomeController(ArticleRepository articleRepository, IMapper mapper, CategoryRepository categoryRepository)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index(int perPage = 13, int page = 1)
        {
            var articles = _mapper.Map <List<ArticleViewModel>>(_articleRepository.GetForPagination(perPage, page));
            var categories = _mapper.Map<List<CategoryViewModel>>(_categoryRepository.GetAll());

            var DataView = new DataArticlesViewModel()
            {
                Articles = articles,
                Categories = categories,
                MyPage = page,
                PerPage = perPage,

            };
            return View(DataView);

        }

        public IActionResult InfoArticle(long IdArticle)
        {
            var myArticle = _articleRepository.Get(IdArticle);
            if (myArticle == null)
            {
                return RedirectToAction("Index");
            }
            return View(_mapper.Map<ArticleViewModel>(myArticle));
        }
    }
}
