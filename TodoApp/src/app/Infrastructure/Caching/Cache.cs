using System;
using System.Threading.Tasks;
using LazyCache;
using Todo.Domain.Common;
using Serilog;

namespace Todo.Infrastructure.Caching
{
    public class Cache : ICache
    {
        private readonly IAppCache _cache;
        private readonly AppSettings _appSettings;
        private readonly ILogger _logger = Log.ForContext<Cache>();

        public Cache(IAppCache cache, AppSettings appSettings)
        {
            _cache = cache;
            _appSettings = appSettings;
        }

        public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> addItemFactory)
        {
            var cachedItem = await _cache.GetAsync<T>(key);

            try
            {
                var expires = SystemTime.UtcNow().AddMinutes(_appSettings.StaticDataCacheExpiryMins);
                cachedItem = await _cache.GetOrAddAsync<T>(key, LoggingWrapper(addItemFactory), expires);
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"Failed to retrieve type '{typeof(T).Name}' from cache.");
            }

            return cachedItem;
        }

        private Func<Task<T>> LoggingWrapper<T>(Func<Task<T>> addItemFactory)
        {
            Task<T> ActionToWrap()
            {
                var typeToCache = typeof(T).Name;
                _logger.Debug($"Attempting to cache {typeToCache}.");
                Task<T> item = addItemFactory();
                _logger.Debug($"Successfully cached {typeToCache}.");

                return item;
            }

            return ActionToWrap;
        }
    }
}