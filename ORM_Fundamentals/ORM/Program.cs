using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ORM.Domain;
using System;
using Microsoft.Extensions.Configuration;

namespace ORM
{
    class Program
    {
        public static readonly IConfiguration Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

        private static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddDbContext<DataContext>(op => op.UseSqlServer(Configuration.GetConnectionString("Mentoring")));
            services.AddTransient<RepositoryService>();
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetService<RepositoryService>();

            foreach (var book in service.GetBooksByAuthor("Author1"))
            {
                Console.Write(book.Name);
            }

            Console.WriteLine();

            foreach (var author in service.GetBooksAndOrdersByMark(2))
            {
                Console.Write(author);
            }
        }
    }
}
