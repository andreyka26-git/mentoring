namespace LibraryNetwork.Interfaces
{
    public interface IFileParserFactory
    {
        IFileParser CreateFileParser(string extension);
    }
}
