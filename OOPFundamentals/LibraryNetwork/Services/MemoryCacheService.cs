using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace LibraryNetwork.Services
{
    public class MemoryCacheService : MemoryCache
    {
        public MemoryCacheService(IOptions<MemoryCacheOptions> optionsAccessor) : base(optionsAccessor)
        {
        }
    }
}
