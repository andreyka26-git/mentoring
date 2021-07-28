using System.Collections.Generic;
using FileSystem.Models;

namespace FileSystem.Interfaces
{
    public interface IFileSystemVisitor
    {
        IEnumerable<SystemItemModel> GetSystemTreeItems(string path);
    }
}
