using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InstagramDownloader.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace InstagramDownloader.Services.Services
{
    public class CachingService : ICachingService
    {
        private IMemoryCache Cache { get; }

        public CachingService(IMemoryCache cache)
        {
            Cache = cache;
        }

        public TResult Get<TResult>(string key)
        {
            return Cache.Get<TResult>(key);
        }

        public void Set<T>(string key, T item)
        {
            if (!Cache.TryGetValue(key, out T cacheEntry)) // Thank you, C# 7.
            {
                cacheEntry = item;
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
                Cache.Set(key, cacheEntry, cacheEntryOptions);
            }
        }
    }
}
