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
            var number = path.Substring(indexCharp + 1, indexPoint - indexCharp - 1);
            return Convert.ToInt32(number);
        }

        public string GetStringModelFromFileName(string fileName)
        {
            var index = fileName.IndexOf("_", StringComparison.Ordinal);
            return fileName[..index];
        }
    }
}
