using System.IO;
using System.Threading.Tasks;
using LibraryNetwork.Interfaces;
using LibraryNetwork.Models;
using Newtonsoft.Json;

namespace LibraryNetwork.Services
{
    public class JsonParser: IFileParser
    {
        private readonly ILibraryCacheable _cacheService;
        private readonly IPathParser _pathParser;
        private readonly IStringToModelConverter _converter;

        public JsonParser(ILibraryCacheable cacheService, IPathParser pathParser, IStringToModelConverter converter)
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

            var jsonString = await File.ReadAllTextAsync(path);
            var stringEntity = _pathParser.GetStringModelFromFileName(Path.GetFileName(path));
            var type = _converter.StringToModelConvert(stringEntity);
            var parsedModel = (LibraryEntity)JsonConvert.DeserializeObject(jsonString, type);
           
            _cacheService.AddLibraryEntityToCache(path, parsedModel);
            return parsedModel;
        }
    }
}
