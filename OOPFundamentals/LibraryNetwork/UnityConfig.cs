using LibraryNetwork.Interfaces;
using LibraryNetwork.Services;
using Unity;

namespace LibraryNetwork
{
    public static class UnityConfig
    {
        public static UnityContainer RegisterTypes()
        {
            var container = new UnityContainer();
            container.RegisterType<ILibraryService, LibraryService>();
            container.RegisterType<IUserInteractable, ConsoleInteraction>();
            container.RegisterType<IPathParser, PathParser>();
            container.RegisterType<IFileParserFactory, FileParserFactory>();
            return container;
        }
    }
}
