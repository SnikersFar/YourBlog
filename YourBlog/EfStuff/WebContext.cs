using Microsoft.EntityFrameworkCore;
using YourBlog.EfStuff.DbModel;

namespace YourBlog.EfStuff
{
    public class WebContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Report> Reports { get; set; }

        public WebContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.Articles).WithOne(a => a.Creator);
            modelBuilder.Entity<Category>().HasMany(c => c.Articles).WithOne(a => a.IsCategory);
            modelBuilder.Entity<Report>().HasOne(r => r.ReportAuthor).WithMany(u => u.Reports);
            modelBuilder.Entity<Report>().HasOne(r => r.Post).WithMany(a => a.Reports);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
