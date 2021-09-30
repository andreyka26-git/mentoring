using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ORM.Domain
{
    public class ContextFactory: IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(Program.Configuration.GetConnectionString("Mentoring"),
                opts => opts.CommandTimeout((int) TimeSpan.FromMinutes(10).TotalSeconds));
            return new DataContext(optionsBuilder.Options);
        }
    }
}
