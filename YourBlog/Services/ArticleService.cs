using System.Collections.Generic;
using YourBlog.EfStuff.DbModel;
using YourBlog.EfStuff.Repositories;

namespace YourBlog.Services
{
    public class ArticleService
    {

        private ArticleRepository _articleRepository;
        private CategoryRepository _categoryRepository;

        public ArticleService(ArticleRepository articleRepository, CategoryRepository categoryRepository)
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
        }


    }
}
