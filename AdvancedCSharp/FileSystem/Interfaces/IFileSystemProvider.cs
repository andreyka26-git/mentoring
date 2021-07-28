using System.IO;

namespace FileSystem.Interfaces
{
    public interface IFileSystemProvider
    {
        FileInfo[] GetFiles(string path);
        DirectoryInfo[] GetDirectories(string path);
    }
}