using Microsoft.EntityFrameworkCore;
using ORM.Models;

namespace ORM.Domain
{
    public class DataContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Article> Articles { get; set; }

        public DataContext(DbContextOptions<DataContext> context) : base(context) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedData();
        }
    }
}
