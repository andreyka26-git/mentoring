using System;
using LibraryNetwork.Interfaces;

namespace LibraryNetwork.Services
{
    public class PathParser : IPathParser
    {
        public int GetIdFromFilePath(string path)
        {
            var indexCharp = path.IndexOf("#", StringComparison.Ordinal);
            var indexPoint = path.IndexOf(".", StringComparison.Ordinal);
            var number = path.Substring(indexCharp, indexCharp + indexPoint + 1);
            return Convert.ToInt32(number);
        }

        public string GetStringLibraryEntity(string path)
        {
            var index = path.IndexOf("_", StringComparison.Ordinal);
            return path.Substring(0, index + 1);
        }
    }
}
