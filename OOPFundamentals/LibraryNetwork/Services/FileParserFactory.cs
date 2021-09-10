using System;
using LibraryNetwork.Interfaces;

namespace LibraryNetwork.Services
{
    public class FileParserFactory: IFileParserFactory
    {
        private readonly ILibraryCacheable _cacheService;
        private readonly IPathParser _pathParser;
        private readonly IStringToModelConverter _converter;

        private const string JsonExtension = ".json";
        private const string XmlExtension = ".xml";

        public FileParserFactory(ILibraryCacheable cacheService, IPathParser pathParser, IStringToModelConverter converter)
        {
            _cacheService = cacheService;
            _pathParser = pathParser;
            _converter = converter;
        }

        public IFileParser CreateFileParser(string extension)
        {
            return extension switch
            {
                JsonExtension => new JsonParser(_cacheService, _pathParser, _converter),
                XmlExtension => new XmlParser(_cacheService, _pathParser, _converter),
                _ => throw new ArgumentException("Incorrect extension passed.")
            };
        }
    }
}
