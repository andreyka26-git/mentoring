namespace LibraryNetwork.Models
{
    public class Newspaper : LibraryEntity
    {
        public override string Id => $"{Publisher}-{Year}-{Number}";
        public string Year { get; set; }
        public string Publisher { get; set; }
        public string Number { get; set; }

        public override string EntityToString()
        {
            var stringRepresentation = base.EntityToString() + '\n';
            stringRepresentation += $"Publisher: {Publisher}, year: {Year}, number: {Number}";
            return stringRepresentation;
        }
    }
}
