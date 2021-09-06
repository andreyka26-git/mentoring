using LibraryNetwork.Models;
using System.Threading.Tasks;

namespace LibraryNetwork.Interfaces
{
    public interface ILibraryService
    {
        Task<LibraryEntity> FoundLibraryEntity(string path, string id);
    }
}
