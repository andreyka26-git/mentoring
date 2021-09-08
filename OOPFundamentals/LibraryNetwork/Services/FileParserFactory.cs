using System;
using LibraryNetwork.Interfaces;

namespace LibraryNetwork.Services
{
    public class FileParserFactory: IFileParserFactory
    {
        private const string JsonExtension = ".json";
        private const string XmlExtension = ".xml";
        private readonly ILibraryCacheable _cacheService;
        private readonly IPathParser _pathParser;

        public FileParserFactory(ILibraryCacheable cacheService, IPathParser pathParser)
        {
            _cacheService = cacheService;
            _pathParser = pathParser;
        }

        public IFileParser CreateFileParser(string extension)
        {
            return extension switch
            {
                JsonExtension => new JsonParser(_cacheService, _pathParser),
                XmlExtension => new XmlParser(_cacheService),
                _ => throw new ArgumentException("Incorrect extension passed.")
            };
        }
    }
}
