using System;
using System.IO;
using System.Threading.Tasks;
using LibraryNetwork.Interfaces;
using LibraryNetwork.Models;

namespace LibraryNetwork.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IFileParserFactory _fileParserFactory;
        private readonly IPathParser _pathParser;

        public LibraryService(IFileParserFactory fileParserFactory, IPathParser pathParser)
        {
            _fileParserFactory = fileParserFactory;
            _pathParser = pathParser;
        }

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
                var number = _pathParser.GetIdFromFilePath(path);
                if (number != parsedId)
                    continue;

                foundPath = filePath;
                break;
            }

            if (string.IsNullOrEmpty(foundPath)) 
                return null;
            
            var parser = _fileParserFactory.CreateFileParser(Path.GetExtension(foundPath));
            return await parser.GetLibraryEntity(foundPath);
        }
    }
}
