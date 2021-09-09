using LibraryNetwork.Interfaces;
using LibraryNetwork.Services;
using Microsoft.Extensions.Caching.Memory;
using Unity;

namespace LibraryNetwork
{
    public static class UnityConfig
    {
        public static UnityContainer RegisterTypes()
        {
            var container = new UnityContainer();
            
            var memoryCache = new MemoryCacheService(new MemoryCacheOptions());
            container.RegisterInstance<IMemoryCache>(memoryCache);
            
            container.RegisterType<IUserInteractable, ConsoleInteraction>();
            container.RegisterType<IPathParser, PathParser>();
            container.RegisterType<IFileParserFactory, FileParserFactory>();
            container.RegisterType<IStringToModelConverter, StringToModelConvertService>();
            container.RegisterType<ILibraryService, LibraryService>();
            container.RegisterType<ILibraryCacheable, LibraryCacheableService>();

            return container;
        }
    }
}
