using System.IO;
using System.Threading.Tasks;
using LibraryNetwork.Interfaces;
using LibraryNetwork.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LibraryNetwork.Services
{
    public class JsonParser: IFileParser
    {
        private readonly ILibraryCacheable _cacheService;

        public JsonParser(ILibraryCacheable cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<int> GetLibraryEntityId(string path)
        {
            var jsonString = await File.ReadAllTextAsync(path);
            var jsonObject = JObject.Parse(jsonString);
            return (jsonObject["id"] ?? -1).Value<int>();
        }

        public async Task<LibraryEntity> GetLibraryEntity(string path)
        {
            var cacheEntity = _cacheService.GetLibraryEntityFromCache(path);
            if (cacheEntity != null)
            {
                return cacheEntity;
            }

            var jsonString = await File.ReadAllTextAsync(path);
            var jsonObject = JsonConvert.DeserializeObject<LibraryEntity>(jsonString);
            _cacheService.AddLibraryEntityToCache(path, jsonObject);
            return jsonObject;
        }
    }
}
