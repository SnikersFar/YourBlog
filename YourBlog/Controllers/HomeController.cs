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
        IMapper _mapper;
        public HomeController(ArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var articles = _mapper.Map<List<ArticleViewModel>>(_articleRepository.GetAll());
            return View(articles);
        }
    }
}
