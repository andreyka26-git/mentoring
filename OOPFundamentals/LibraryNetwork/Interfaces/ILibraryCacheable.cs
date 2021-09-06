using LibraryNetwork.Models;

namespace LibraryNetwork.Interfaces
{
    public interface ILibraryCacheable
    {
        void AddLibraryEntityToCache(string path, LibraryEntity entity);
        LibraryEntity GetLibraryEntityFromCache(string path);
    }
}
