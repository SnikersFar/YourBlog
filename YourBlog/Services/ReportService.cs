using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using YourBlog.EfStuff.DbModel;
using YourBlog.EfStuff.Repositories;
using YourBlog.Models;

namespace YourBlog.Services
{
    public class ReportService
    {
        private UserRepository _userRepository;
        private ReportRepository _reportRepository;
        private ArticleRepository _articleRepository;
        private IMapper _mapper;

        public ReportService(UserRepository userRepository, ReportRepository reportRepository, IMapper mapper, ArticleRepository articleRepository)
        {
            _userRepository = userRepository;
            _reportRepository = reportRepository;
            _mapper = mapper;
            _articleRepository = articleRepository;
        }
        public IList<ReportViewModel> GetAllReports()
        {
            var reports = _reportRepository.GetAll();
            return _mapper.Map<List<ReportViewModel>>(reports);
        }
        public void Ban(BanViewModel banView)
        {
            var user = _userRepository.Get(banView.UserId);
            if (user is null)
                return;
            if (banView.TypeBan.HasFlag(TypeBan.User))
            {
                user.IsActive = false;
                
            }

            if (banView.TypeBan.HasFlag(TypeBan.Article))
            {
                var article = _articleRepository.Get(banView.ArticleId);
                article.Reports.ForEach(a => a.IsActive = false);
                _articleRepository.Save(article);

                user.Articles.FirstOrDefault(a => a.Id == banView.ArticleId).IsActive = false;
            }

            if (banView.TypeBan.HasFlag(TypeBan.AllArticles))
            {
                var article = _articleRepository.Get(banView.ArticleId);
                article.Reports.ForEach(a => a.IsActive = false);
                _articleRepository.Save(article);

                user.Articles.ForEach(a => a.IsActive = false);
            }
            _userRepository.Save(user);

        }

        public void AddReport(ReportRequestViewModel reportRequest)
        {
            var post = _articleRepository.Get(reportRequest.ArticleId);
            if (post is null)
                return;
            var report = new Report()
            {
                IsActive = true,
                Message = reportRequest.ReportMessage,
                Post = post,
                ReportAuthor = _userRepository.Get(reportRequest.ReportAuthorId),
            };
            _reportRepository.Save(report);
        }
    }
}
