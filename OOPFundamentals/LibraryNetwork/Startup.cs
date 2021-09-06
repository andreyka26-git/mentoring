using LibraryNetwork.Interfaces;

namespace LibraryNetwork
{
    public class Startup
    {
        private readonly IUserInteractable _view;
        private readonly ILibraryService _libraryService;

        public Startup(IUserInteractable view, ILibraryService libraryService)
        {
            _view = view;
            _libraryService = libraryService;
        }

        public async void Run(string storePath)
        {
            var id = _view.GetId();
            var entity = await _libraryService.FoundLibraryEntity(storePath, id);
            _view.PrintLibraryEntity(entity);
        }
    }
}
