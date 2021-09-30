using System;

namespace ORM.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Mark { get; set; }
        public string ReviewerName { get; set; }
        public DateTime ReviewDateTime { get; set; }
        public virtual Book Book { get; set; }
        public int BookId { get; set; }
    }
}
