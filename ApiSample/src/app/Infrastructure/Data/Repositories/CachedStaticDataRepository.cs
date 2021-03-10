using System;
using System.Threading;
using System.Threading.Tasks;
using ApiSample.Domain.Abstractions;
using ApiSample.Domain.Model.StaticData;
using ApiSample.Infrastructure.Caching;

namespace ApiSample.Infrastructure.Data.Repositories
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