using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Aggregates.EmployeeAggregate;
using WebAPI.Domain.Aggregates.ProjectAggregate;

namespace WebAPI.Infrastructure
{
    public class DataContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DataContext(DbContextOptions<DataContext> context) : base(context) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasOne<Project>().WithMany().HasForeignKey(f => f.ProjectId);
        }
    }
}
