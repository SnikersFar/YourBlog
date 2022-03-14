using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourBlog.EfStuff;
using YourBlog.EfStuff.DbModel;
using YourBlog.EfStuff.Repositories;
using YourBlog.Models;
using YourBlog.Services;

namespace YourBlog
{
    public class Startup
    {
        public const string AuthCoockieName = "User";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyBlog;Integrated Security=True;";
            services.AddDbContext<WebContext>(x => x.UseSqlServer(connectString));

            services.AddScoped<UserRepository>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<ArticleRepository>();
            services.AddScoped<UserService>();
            services.AddScoped<ArticleService>();

            var provider = new MapperConfigurationExpression();

            provider.CreateMap<Article, ArticleViewModel>()
                .ForMember(aView => aView.CategoryId, db => db.MapFrom(art => art.IsCategory.Id))
                .ForMember(aView => aView.CategoryName, db => db.MapFrom(art => art.IsCategory.Name))
                .ForMember(aView => aView.CreatorId, db => db.MapFrom(art => art.Creator.Id));

            provider.CreateMap<ArticleViewModel, Article>();

            provider.CreateMap<Category, CategoryViewModel>()
                .ForMember(cView => cView.CountArticles, db => db.MapFrom(cat => cat.Articles.Where(a => a.IsActive).ToList().Count));

            var mapperConfiguration = new MapperConfiguration(provider);
            var mapper = new Mapper(mapperConfiguration);
            services.AddScoped<IMapper>(x => mapper);

            services.AddAuthentication(AuthCoockieName)
               .AddCookie(AuthCoockieName, config =>
               {
                   config.LoginPath = "/Admin/Login";
                   config.AccessDeniedPath = "/Home/Index";
                   config.Cookie.Name = "AuthSweet";
               });
            services.AddSignalR();
            services.AddHttpContextAccessor();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            }); 
        }
    }
}
