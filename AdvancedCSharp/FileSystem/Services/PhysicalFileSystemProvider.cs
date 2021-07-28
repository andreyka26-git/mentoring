using System.IO;
using FileSystem.Interfaces;

namespace FileSystem.Services
{
    public class PhysicalFileSystemProvider : IFileSystemProvider
    {
        public FileInfo[] GetFiles(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            return directoryInfo.GetFiles();
        }

        public DirectoryInfo[] GetDirectories(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            return directoryInfo.GetDirectories();
        }
    }
}