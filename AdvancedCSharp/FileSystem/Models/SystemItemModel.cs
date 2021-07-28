using System.IO;

namespace FileSystem.Models
{
    public class SystemItemModel
    {
        public SystemItemModel(string path, string name, bool isFile, bool isDirectory)
        {
            Path = path;
                .....
        }

        public string Path { get; private set; }
        public string Name { get; private set; }
        //TODO set when initialize object
        public bool IsFile => File.Exists(Path);
        //TODO set when initialize object
        public bool IsFolder => Directory.Exists(Path);
    }
}