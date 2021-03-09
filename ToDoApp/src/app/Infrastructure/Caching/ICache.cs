using System;
using System.Threading.Tasks;

namespace ToDo.Infrastructure.Caching
{
    public interface ICache
    {
        Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> addItemFactory);
    }
}