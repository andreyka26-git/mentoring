using System.IO;

namespace FileSystem.Models
{
    public class SystemItemModel
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public bool IsFile => File.Exists(Path);
        public bool IsFolder => Directory.Exists(Path);
    }
}