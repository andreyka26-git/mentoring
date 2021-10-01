using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ORM.Domain;
using ORM.Models;

namespace ORM
{
    public class RepositoryService
    {
        private readonly DataContext _context;

        public RepositoryService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetBooksByAuthor(string authorName)
        {
            return _context.Books.Where(b => b.Author == authorName);
        }

        public IEnumerable<Book> GetBooksOrderByDate()
        {
            using var db = new SqlConnection(Program.Configuration.GetConnectionString("Mentoring"));
            return db.Query<Book>(
                "SELECT * FROM Books inner join Reviews on Books.Id = Reviews.BookId ORDER BY Reviews.ReviewDateTime");

        }
    }
}
