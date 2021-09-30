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

        public IEnumerable<string> GetBooksAndOrdersByMark(int mark)
        {
            using var db = new SqlConnection(Program.Configuration.GetConnectionString("Mentoring"));
            return db.Query<string>(
                "SELECT Books.Author FROM Books inner join Reviews on Books.Id = Reviews.BookId WHERE Reviews.Mark = @mark",
                new {mark});

        }
    }
}
