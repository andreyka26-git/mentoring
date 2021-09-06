using System;

namespace LibraryNetwork.Models
{
    public class LibraryEntity
    {
        public virtual string Id { get; set; }
        public string Name { get; set; }
        public virtual string EntityToString()
        {
            return $"Name: {Name}";
        }

        public virtual DateTime GetExpiration() => DateTime.MinValue;
    }
}
