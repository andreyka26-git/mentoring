namespace LibraryNetwork.Interfaces
{
    public interface IPathParser
    {
        public int GetIdFromFilePath(string path);
        public string GetStringLibraryEntity(string path);
        public string StringToModelConvert(string name);
    }
}
