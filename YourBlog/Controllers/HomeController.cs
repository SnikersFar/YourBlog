using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
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
            var CountOfArticles = _articleRepository.Count();
            var articles = _mapper.Map <List<ArticleViewModel>>(_articleRepository.GetForPagination(perPage, page));
            var categories = _mapper.Map<List<CategoryViewModel>>(_categoryRepository.GetAll());
            var countOfPages = Math.Ceiling((CountOfArticles * 1.0) / (perPage * 1.0));

            var DataView = new DataArticlesViewModel()
            {
                Articles = articles,
                Categories = categories,
                MyPage = page,
                PerPage = perPage,
                CountPages = Convert.ToInt32(countOfPages),

            };
            return View(DataView);

        }
        [HttpPost]
        public IActionResult Index(DataArticlesViewModel dataView)
        {
            var ViewArticles = _mapper.Map<List<ArticleViewModel>>(_articleRepository.GetByFilter(dataView));
            var categories = _mapper.Map<List<CategoryViewModel>>(_categoryRepository.GetAll());

            var CountOfArticles = _articleRepository.Count();
            var countOfPages = Math.Ceiling((CountOfArticles * 1.0) / (13 * 1.0));


            var DataView = new DataArticlesViewModel()
            {
                Articles = ViewArticles,
                Categories = categories,
                MyPage = 1,
                PerPage = 13,
                CountPages = Convert.ToInt32(countOfPages),

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
