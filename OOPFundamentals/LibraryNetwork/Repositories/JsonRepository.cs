using System.IO;
using LibraryNetwork.Interfaces;
using LibraryNetwork.Models;
using Newtonsoft.Json;


namespace LibraryNetwork.Repositories
{
    public class JsonRepository : ILibraryEntityRepository
    {
        public LibraryEntity GetLibraryEntity(string path)
        {
            var jsonString = File.ReadAllText(path);
            var jsonObject = JsonConvert.DeserializeObject<LibraryEntity>(jsonString);
            return jsonObject;
        }
    }
}
