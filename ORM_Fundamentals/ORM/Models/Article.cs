namespace ORM.Models
{
    public class Article: LibraryItem
    {
        public int Citations { get; set; }
        public string Content { get; set; }
    }
}
