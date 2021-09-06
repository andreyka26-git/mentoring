namespace LibraryNetwork.Models
{
    public class LocalizedBook : Book
    {
        public string Country { get; set; }
        public string LocalPublisher { get; set; }

        public override string EntityToString()
        {
            var stringRepresentation = base.EntityToString() + '\n';
            stringRepresentation += $"Country: {Country}, Local publisher: {LocalPublisher}";
            return stringRepresentation;
        }
    }
}
