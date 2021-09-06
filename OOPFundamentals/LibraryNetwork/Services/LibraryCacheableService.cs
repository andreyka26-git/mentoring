using LibraryNetwork.Interfaces;
using LibraryNetwork.Models;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace LibraryNetwork.Services
{
    public class LibraryCacheableService : ILibraryCacheable
    {
        private readonly IMemoryCache _cache;
        public LibraryCacheableService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void AddLibraryEntityToCache(string path, LibraryEntity entity)
        {
            _cache.Set(path, entity, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3)
            });
        }

        public LibraryEntity GetLibraryEntityFromCache(string path)
        {
            return _cache.Get<LibraryEntity>(path);
        }
    }
}
