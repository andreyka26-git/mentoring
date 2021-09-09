using Unity;

namespace LibraryNetwork
{
    class Program
    {
        private static readonly string _storePath = "C:\\Users\\Serhii_Yurko\\Desktop\\Library";
        static void Main(string[] args)
        {
            var container = UnityConfig.RegisterTypes();
            var startup = container.Resolve<Startup>();
            startup.Run(_storePath);
        }
    }
}
