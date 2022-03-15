using System;
using System.Collections.Generic;
using System.Linq;
using WebMaze.EfStuff.Repositories;
using YourBlog.EfStuff.DbModel;
using YourBlog.Models;

namespace YourBlog.EfStuff.Repositories
{
    public class ArticleRepository : BaseRepository<Article>
    {
        public ArticleRepository(WebContext webContext) : base(webContext)
        {
        }


        public List<Article> GetByFilter(DataArticlesViewModel dataView )
        {
            List<Article> articles = GetAll();
            if (dataView.FiltByCategory)
            {
                articles = FiltByCategory(articles, dataView.CategoryIdFind);
            }
            if (dataView.FiltByDate)
            {
                articles = FiltByDate(articles, dataView.FromDateFind, dataView.ToDateFind);
            }
            if (dataView.FiltByTags)
            {
                articles = FiltByTags(articles, dataView.TagFind);
            }
            return articles;
        }

        private List<Article> FiltByCategory(List<Article> articles, long categoryId) => articles.Where(a => a.IsCategory.Id == categoryId).ToList();
        private List<Article> FiltByTags(List<Article> articles, string FiltTags)
        {
            var Tags = FiltTags.Split();
            var TagArticles = articles.Where(a =>
          {
              var aTags = a.Tags.Split();
              var result = Tags.Intersect(aTags);
              return result.Any();
          }).ToList();
            return TagArticles;
        }
        private List<Article> FiltByDate(List<Article> articles, DateTime fromDate, DateTime toDate) => articles.Where(a => a.CreatedDate > fromDate && a.CreatedDate < toDate).ToList();


    }
}
