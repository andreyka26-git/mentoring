using System.Collections.Generic;

namespace ORM.Models
{
    public class Book: LibraryItem
    {
        public string Author { get; set; }
        public string Name { get; set; }
        public virtual List<Review> Reviews { get; set; } = new();
    }
}
