using System;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Domain.Abstractions;
using ToDo.Domain.Model.StaticData;
using ToDo.Infrastructure.Caching;

namespace ToDo.Infrastructure.Data.Repositories
{
    public class CachedStaticDataRepository : IStaticDataRepository
    {
        private readonly IStaticDataRepository _repository;
        private readonly ICache _cache;

        public CachedStaticDataRepository(IStaticDataRepository repository, ICache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<StaticDataStore> GetStaticDataAsync(CancellationToken token)
        {
            Func<Task<StaticDataStore>> actionToCache = () => _repository.GetStaticDataAsync(token);

            var cachedData = await _cache.GetOrAddAsync("StaticData", actionToCache);

            return cachedData;
        }
    }
}