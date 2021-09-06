using LibraryNetwork.Models;

namespace LibraryNetwork.Interfaces
{
    public interface ILibraryEntityRepository
    {
        LibraryEntity GetLibraryEntity(string path);
    }
}
