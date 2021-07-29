using System;

namespace FileSystem.Models
{
    public class SystemItemModel
    {
        public SystemItemModel(string path, string name, bool isFile)
        {
            Path = path;
            Name = name;
            IsFile = isFile;
            IsFolder = !isFile;
        }

        public string Path { get; }
        public string Name { get; }
        public bool IsFile { get; }
        public bool IsFolder { get; }

        public override bool Equals(object obj)
        {
            return obj is SystemItemModel model && Path.Equals(model.Path) && Name.Equals(model.Name) &&
                   IsFile.Equals(model.IsFile) && IsFolder.Equals(model.IsFolder);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Path, Name, IsFile, IsFolder);
        }
    }
}