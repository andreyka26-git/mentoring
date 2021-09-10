using System;
using System.Collections.Generic;
using LibraryNetwork.Interfaces;
using LibraryNetwork.Models;

namespace LibraryNetwork.Services
{
    public class StringToModelConvertService: IStringToModelConverter
    {
        private readonly Dictionary<string, Type> _stringModelPairs = new Dictionary<string, Type>
        {
            {"book", typeof(Book)},
            {"localizedbook", typeof(LocalizedBook)},
            {"newspaper", typeof(Newspaper)},
            {"patent", typeof(Patent)}
        };

        public Type StringToModelConvert(string name)
        {
            var nameToLower = name.ToLower();
            if (!_stringModelPairs.TryGetValue(nameToLower, out var nameClass))
                throw new ArgumentException("This type of name is not support.");

            return nameClass;
        }
    }
}
