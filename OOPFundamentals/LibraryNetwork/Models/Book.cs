using System.Collections.Generic;

namespace LibraryNetwork.Models
{
    public class Book : LibraryEntity
    {
        public List<string> Authors { get; set; }
        public string Publisher { get; set; }
        public int PagesCount { get; set; }
        public int Year { get; set; }
        public string Isbn { get; set; }

        public override string EntityToString()
        {
            var stringRepresentation = base.EntityToString() + '\n';
            stringRepresentation += "Authors: ";
            Authors.ForEach(author => stringRepresentation += $"{author} ");
            stringRepresentation = stringRepresentation.TrimEnd() + '\n';
            stringRepresentation += $"Publisher: {Publisher}, year: {Year}, pages: {PagesCount}, ISBN: {Isbn}";
            return stringRepresentation;
        }
    }
}
