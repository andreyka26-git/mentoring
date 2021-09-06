using Unity;

namespace LibraryNetwork
{
    class Program
    {
        private static readonly string _storePath = "";
        static void Main(string[] args)
        {
            var container = UnityConfig.RegisterTypes();
            var startup = container.Resolve<Startup>();
            startup.Run(_storePath);
        }
    }
}
