using System;
using System.Collections.Generic;
using LibraryNetwork.Interfaces;
using LibraryNetwork.Models;

namespace LibraryNetwork.Services
{
    public class PathParser : IPathParser
    {
        private readonly Dictionary<string, string> _stringModelPairs = new Dictionary<string, string>
        {
            {"book", nameof(Book)},
            {"localizedbook", nameof(LocalizedBook)},
            {"newspaper", nameof(Newspaper)},
            {"patent", nameof(Patent)}
        };

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

        public string StringToModelConvert(string name)
        {
            var nameToLower = name.ToLower();
            if (!_stringModelPairs.TryGetValue(nameToLower, out var nameClass))
                throw new ArgumentException("This type of name is not support.");

            return nameClass;
        }
    }
}
