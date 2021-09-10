namespace LibraryNetwork.Interfaces
{
    public interface IPathParser
    {
        public int GetIdFromFilePath(string path);
        public string GetStringModelFromFileName(string path);
    }
}
