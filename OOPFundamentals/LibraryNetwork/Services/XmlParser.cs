using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using LibraryNetwork.Interfaces;
using LibraryNetwork.Models;

namespace LibraryNetwork.Services
{
    public class XmlParser : IFileParser
    {
        private readonly ILibraryCacheable _cacheService;
        private readonly IPathParser _pathParser;
        private readonly IStringToModelConverter _converter;

        public XmlParser(ILibraryCacheable cacheService, IPathParser pathParser, IStringToModelConverter converter)
        {
            _cacheService = cacheService;
            _pathParser = pathParser;
            _converter = converter;
        }

        public async Task<LibraryEntity> GetLibraryEntity(string path)
        {
            var cacheEntity = _cacheService.GetLibraryEntityFromCache(path);
            if (cacheEntity != null)
            {
                return cacheEntity;
            }

            var xmlString = await File.ReadAllTextAsync(path);
            var stringEntity = _pathParser.GetStringModelFromFileName(Path.GetFileName(path));
            var type = _converter.StringToModelConvert(stringEntity);

            LibraryEntity parsedModel;
            var xRoot = new XmlRootAttribute();
            xRoot.ElementName = "root";
            var serializer = new XmlSerializer(type, xRoot);
            using (var reader = new StringReader(xmlString))
            {
                parsedModel = (LibraryEntity)serializer.Deserialize(reader);
            }

            _cacheService.AddLibraryEntityToCache(path, (LibraryEntity)parsedModel);
            return (LibraryEntity)parsedModel;
        }
    }
}
