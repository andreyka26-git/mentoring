using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using LibraryNetwork.Interfaces;
using LibraryNetwork.Models;

namespace LibraryNetwork.Services
{
    public class XmlParser: IFileParser
    {
        private readonly ILibraryCacheable _cacheService;

        public XmlParser(ILibraryCacheable cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<LibraryEntity> GetLibraryEntity(string path)
        {
            var cacheEntity = _cacheService.GetLibraryEntityFromCache(path);
            if (cacheEntity != null)
            {
                return cacheEntity;
            }

            var xmlString = await File.ReadAllTextAsync(path);
            var text = new XmlTextReader(xmlString); 
            var serializer = new XmlSerializer(typeof(LibraryEntity));
            return serializer.Deserialize(text) as LibraryEntity;
        }
    }
}
