using LibraryNetwork.Models;

namespace LibraryNetwork.Interfaces
{
    public interface IUserInteractable
    {
        public string GetId();
        public void PrintLibraryEntity(LibraryEntity entity);
    }
}
