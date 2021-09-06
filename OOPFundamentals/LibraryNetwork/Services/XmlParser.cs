using System.Threading.Tasks;
using LibraryNetwork.Interfaces;
using LibraryNetwork.Models;

namespace LibraryNetwork.Services
{
    public class XmlParser: IFileParser
    {
        public Task<int> GetLibraryEntityId(string path)
        {
            throw new System.NotImplementedException();
        }

        public Task<LibraryEntity> GetLibraryEntity(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}
