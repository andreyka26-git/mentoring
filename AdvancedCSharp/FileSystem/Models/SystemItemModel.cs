using System.IO;

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

        //TODO implement equals
        public override bool Equals(object obj)
        {
            if (!(obj is SystemItemModel model))
                return false;

            return model.Path == Path ? true : false;
        }
    }
}