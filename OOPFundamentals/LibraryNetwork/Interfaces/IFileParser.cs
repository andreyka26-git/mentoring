using System.Threading.Tasks;
using LibraryNetwork.Models;

namespace LibraryNetwork.Interfaces
{
    public interface IFileParser
    {
        Task<int> GetLibraryEntityId(string path);
        Task<LibraryEntity> GetLibraryEntity(string path);
    }
}
