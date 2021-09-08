using System;
using LibraryNetwork.Interfaces;

namespace LibraryNetwork.Services
{
    public class FileParserFactory: IFileParserFactory
    {
        private const string JsonExtension = ".json";
        private const string XmlExtension = ".xml";
        private readonly ILibraryCacheable _cacheService;

        public FileParserFactory(ILibraryCacheable cacheService)
        {
            _cacheService = cacheService;
        }

        public IFileParser CreateFileParser(string extension)
        {
            return extension switch
            {
                JsonExtension => new JsonParser(_cacheService),
                XmlExtension => new XmlParser(_cacheService),
                _ => throw new ArgumentException("Incorrect extension passed.")
            };
        }
    }
}
