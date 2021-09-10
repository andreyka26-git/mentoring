using System.Threading.Tasks;
using LibraryNetwork.Models;

namespace LibraryNetwork.Interfaces
{
    public interface IFileParser
    {
        Task<LibraryEntity> GetLibraryEntity(string path);
    }
}
