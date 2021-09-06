using System;
using System.IO;
using System.Threading.Tasks;
using LibraryNetwork.Interfaces;
using LibraryNetwork.Models;

namespace LibraryNetwork.Services
{
    public class LibraryService : ILibraryService
    {
        private IFileParser _fileParser;
        private readonly string JsonExtension = ".json";
        private readonly string XmlExtension = ".xml";

        public async Task<LibraryEntity> FoundLibraryEntity(string path, string id)
        {
            if (!Directory.Exists(path))
                throw new ArgumentException("Directory doesn't exist by this path.");

            if (!int.TryParse(id, out var parsedId))
                throw new ArgumentException("Id is incorrect.");

            var filesPath = Directory.GetFiles(path);
            var foundPath = string.Empty;
            foreach (var filePath in filesPath)
            {
                var foundId = await _fileParser.GetLibraryEntityId(filePath);
                if (foundId != parsedId)
                    continue;

                foundPath = filePath;
                break;
            }

            if (!string.IsNullOrEmpty(foundPath))
            {
                var fileExtension = Path.GetExtension(foundPath);
                if (fileExtension == JsonExtension)
                    //_fileParser = new JsonParser();
                
                return await _fileParser.GetLibraryEntity(foundPath);
            }

            return null;
        }
    }
}
