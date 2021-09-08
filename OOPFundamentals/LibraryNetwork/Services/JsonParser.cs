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

        public JsonParser(ILibraryCacheable cacheService, IPathParser pathParser)
        {
            _cacheService = cacheService;
            _pathParser = pathParser;
        }

        public async Task<LibraryEntity> GetLibraryEntity(string path)
        {
            var cacheEntity = _cacheService.GetLibraryEntityFromCache(path);
            if (cacheEntity != null)
            {
                return cacheEntity;
            }

            var jsonString = await File.ReadAllTextAsync(path);
            var stringEntity = _pathParser.GetStringLibraryEntity(path);
            var jsonObject = JsonConvert.DeserializeObject<LibraryEntity>(jsonString);
            _cacheService.AddLibraryEntityToCache(path, jsonObject);
            return jsonObject;
        }
    }
}
